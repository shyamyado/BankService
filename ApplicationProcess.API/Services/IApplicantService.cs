using ApplicationProcess.API.Model;

namespace ApplicationProcess.API.Services
{
    public interface IApplicantService
    {
        public Task<Application> GetApplicationById(int id);
        public Task<Customer> CompleteCustomerDetails(Customer customer);
        public Task<Customer> AddCustomer(Customer customer);
    }
}
