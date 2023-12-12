using ApplicationProcess.API.Model;

namespace ApplicationProcess.API.Infrastructure.Repositories
{
    public interface IApplicantRepository
    {
        public Task<List<Application>> GetAllNewApplication();
        public Task<Application> GetApplicationById(int id);
        public Task<Customer> UpdateCustomerDetails(Customer customer);
        public Task<Customer> AddCustomer(Customer customer);
    }
}
