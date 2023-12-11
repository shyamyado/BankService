using Catalog.API.Models;

namespace Catalog.API.Services
{
    public interface IApplicationService
    {
        public Task<string> ApplyCard(ApplicationForm form);
    }
}
