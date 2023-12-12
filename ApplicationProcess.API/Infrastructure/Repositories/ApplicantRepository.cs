using ApplicationProcess.API.Model;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace ApplicationProcess.API.Infrastructure.Repositories
{

    public class ApplicantRepository : IApplicantRepository
    {
        private readonly ApplicationDBContext _dBContext;
        private readonly ILogger<ApplicantRepository> _logger;

        public ApplicantRepository(ApplicationDBContext dBContext, ILogger<ApplicantRepository> logger)
        {
            _dBContext = dBContext;
            _logger = logger;
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            try
            {
                var result = await _dBContext.Customers.AddAsync(customer);
                await _dBContext.SaveChangesAsync();
                _logger.LogInformation("Customer added.");
                return result.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding customer.");
                throw new Exception("Error adding customer.");
            }
        }

        public async Task<List<Model.Application>> GetAllNewApplication()
        {
            try
            {
                var result = await _dBContext.Applications.ToListAsync();
                return result;
            }
            catch (Exception )
            {
                _logger.LogError("Error while retrieving Applications");
                throw;
            }
        }

        public async Task<Model.Application> GetApplicationById(int id)
        {
            try
            {
                var res = await _dBContext.Applications.FirstOrDefaultAsync(a => a.Id == id);
                if (res == null)
                {
                    _logger.LogWarning($"Application with ID {id} not found."); 
                }
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving application with ID {id}.");
                throw new ApplicationException($"Error retrieving application with ID {id}. See logs for details.", ex);
            }

        }

        public async Task<Customer> UpdateCustomerDetails(Customer customer)
        {
            try
            {
                var item = await _dBContext.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id);
                _dBContext.Entry(item).CurrentValues.SetValues(customer);
                await _dBContext.SaveChangesAsync();
                _logger.LogInformation("Customer data Saved.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating customer details.");
                throw new Exception("Error updating customer details.");
            }
        }
    }
}
