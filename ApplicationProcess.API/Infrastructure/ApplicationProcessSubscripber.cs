
using ApplicationProcess.API.Infrastructure.Repositories;
using ApplicationProcess.API.Model;
using ApplicationProcess.API.Services;
using Microsoft.EntityFrameworkCore.Metadata;
using NLog.Config;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQEventBus;
using System.Text;

namespace ApplicationProcess.API.Infrastructure
{
    public class ApplicationProcessSubscripber : BackgroundService
    {
        private readonly IRabbitMQPersistantConnection _rabbitMQPersistantConnection;
        private readonly ILogger<ApplicationProcessSubscripber> _logger;
        private readonly IApplicantRepository _applicantRepository;

        public ApplicationProcessSubscripber(IRabbitMQPersistantConnection rabbitMQPersistantConnection, ILogger<ApplicationProcessSubscripber> logger, IServiceScopeFactory factory)
        {
            _logger = logger;
            _rabbitMQPersistantConnection = rabbitMQPersistantConnection;
            _applicantRepository = factory.CreateScope().ServiceProvider.GetRequiredService<IApplicantRepository>();

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Application Processing Subscriber is starting.");

            try
            {
                SubscribeToQueue();
                _logger.LogInformation("Processed a message.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error starting Application Processing Subscriber: {ex.Message}");
            }

            return Task.CompletedTask;
        }

        private void SubscribeToQueue()
        {
            string eventMsgQueueName = "new_application";
            _rabbitMQPersistantConnection.Subscribe(eventMsgQueueName, HandleMessage);
            _logger.LogInformation("Application Processing Subscriber is working.");
        }

        private async void HandleMessage(string message)
        {
            Console.WriteLine($"====ApplicationProcessingsubscriber============Received message: {message}");
            //    // TODO: Add logic here to process the received message into repository
            Console.WriteLine(message.GetType().Name);
            Customer customer = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(message);
            _applicantRepository.AddCustomer(customer);


        }


    }
}
