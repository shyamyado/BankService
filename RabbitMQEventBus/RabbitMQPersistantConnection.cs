using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabbitMQEventBus
{
    public class RabbitMQPersistantConnection : IRabbitMQPersistantConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;

        public bool IsConnected => _connection is { IsOpen: true };

        public RabbitMQPersistantConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentException(nameof(connectionFactory));
        }

        public void Publish(string eventMsgQueueName, object message)
        {
            if (!IsConnected)
            {
                TryConnect();
            }

            var policy = RetryPolicy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    Console.WriteLine("RabbitMQ Client could not connect after {TimeOut}s", $"{time.TotalSeconds: n1}");
                });


            var channel = CreateModel();

            var body = JsonSerializer.SerializeToUtf8Bytes(message);

            channel.QueueDeclare(queue: eventMsgQueueName,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            policy.Execute(() =>
            {
                channel.BasicPublish(exchange: string.Empty,
                                     routingKey: eventMsgQueueName,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine($"=======>Sent {message}");
            });
        }
        public void Subscribe(string eventMsgQueueName, Action<string> handleMessage)
        {
            if (!IsConnected)
            {
                TryConnect();
            }
            var _channel = CreateModel();

            _channel.QueueDeclare(queue: eventMsgQueueName,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received message: {message}");
                handleMessage(message);
            };
            _channel.BasicConsume(queue: eventMsgQueueName,
                autoAck: true,
                consumer: consumer);
            //_channel.Close();
        }

        public bool TryConnect()
        {
            var policy = RetryPolicy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    Console.WriteLine("RabbitMQ Client could not connect after {TimeOut}s", $"{time.TotalSeconds: n1}");
                });

            policy.Execute(() =>
            {
                _connection = _connectionFactory.CreateConnection();
            });

            return IsConnected;
        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connection is available to perform this action");
            }
            return _connection.CreateModel();
        }
    }
}
