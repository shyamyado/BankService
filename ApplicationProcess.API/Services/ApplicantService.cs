using ApplicationProcess.API.Infrastructure.Repositories;
using ApplicationProcess.API.Model;

namespace ApplicationProcess.API.Services
{

    public class ApplicantService : IApplicantService
    {

        private readonly IApplicantRepository _repository;
        private readonly ILogger<ApplicantService> _logger;

        public ApplicantService(ILogger<ApplicantService> logger, IApplicantRepository applicantRepository)
        {
            _logger = logger;
            _repository = applicantRepository;
        }

        public Task<int> GetApplicationByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Application> GetApplicationById(int id)
        {
            try
            {
                var result = await _repository.GetApplicationById(id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR : {ex.Message}");
                throw new Exception("Failed to retrieve items due to an unexpected error.", ex);
            }
        }


        public async Task<Customer> CompleteCustomerDetails(Customer customer)
        {
            try
            {
                var item = await _repository.GetApplicationById(customer.Id);
                if (item == null)
                {
                    return await AddCustomer(customer);
                }
                var res = await _repository.UpdateCustomerDetails(customer);
                return res;
            }
            catch (Exception)
            {
                _logger.LogError("there was an error while updating the Customer details.");
                throw;
            }

        }

        public Task<Customer> AddCustomer(Customer customer)
        {
            try
            {
                var res = _repository.AddCustomer(customer);
                _logger.LogInformation($"New Application recieved {customer.Name}, {customer.Phone}");
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError("there was an error while Adding Customer.");
                throw;
            }
        }
    }
}
