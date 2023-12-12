using ApplicationProcess.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationProcess.API.Services
{
    public interface IAdminService
    {
        public Task<IActionResult> AddStatus(Status status);
        public Task<IActionResult> RenameStatus(int id, Status status);
    }
}
