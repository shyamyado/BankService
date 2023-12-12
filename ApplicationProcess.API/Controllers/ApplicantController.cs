using ApplicationProcess.API.Model;
using ApplicationProcess.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApplicationProcess.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly ILogger<ApplicantController> _logger;
        private readonly IApplicantService _applicantService;

        public ApplicantController(ILogger<ApplicantController> logger, IApplicantService applicantService)
        {
            _logger = logger;
            _applicantService = applicantService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<Application>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<string>> GetApplicationById([FromRoute]int id)
        {
            try
            {
                var res = await _applicantService.GetApplicationById(id);
                if (res == null)
                {
                    _logger.LogInformation("Application not Found or does not exist.");
                    return NotFound("Application not Found or does not exist.");
                }
                _logger.LogInformation($"SUCCESS: {res}");
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Application Number {id}.", ex);
                throw new Exception($"Error while retrieving Application Number {id}.");
            }
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CompleteCustomerDetails([FromBody]Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest("Please provide Application details");
                }
                var result = await _applicantService.CompleteCustomerDetails(customer);
                _logger.LogInformation($"SUCCESS: {result}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while submitting Customer details {customer.Id}.", ex);
                throw new Exception($"Error while submitting Customer details {customer.Id}.");
            }
        }


    }
}

