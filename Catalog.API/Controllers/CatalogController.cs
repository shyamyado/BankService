using Microsoft.AspNetCore.Mvc;
using NLog;
using Catalog.API.Models;
using Catalog.API.Services;
using Catalog.API.ViewModels;
using System.Net;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : Controller
    {

        private readonly ICatalogService _catalogService;
        private readonly IApplicationService _applicationService;
        private readonly IItemPriceChangeService _itemPriceChangeService;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ICatalogService catalogService, IApplicationService applicationService, IItemPriceChangeService itemPriceChangeService, ILogger<CatalogController> logger)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
            _itemPriceChangeService = itemPriceChangeService ?? throw new ArgumentNullException(nameof(itemPriceChangeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ItemViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<ItemViewModel>>> GetAllCatalogItems([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var res = await _catalogService.GetAllItems(page, pageSize);
                if (res == null)
                {
                    _logger.LogInformation("No items found.");
                    return NotFound("No items found.");
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving catalog items.");
                return StatusCode(500, "Failed to retrieve catalog items due to an unexpected error.");
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ItemViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ItemViewModel>> GetCatalogItemById([FromRoute] int id)
        {
            try
            {
                var res = await _catalogService.GetItemById(id);
                if (res == null)
                {
                    _logger.LogInformation($"Item with ID {id} not found.");
                    return NotFound("Item not Found");
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving catalog item with ID {id}.");
                return StatusCode(500, $"Failed to retrieve catalog item with ID {id} due to an unexpected error.");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ItemViewModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ItemViewModel>> AddCatalogItem(ItemCreate item)
        {
            try
            {
                if (item == null)
                {
                    _logger.LogWarning("New Item cannot be null.");
                    return BadRequest(new { message = "New Item cannot be null" });
                }
                var newItem = await _catalogService.AddItem(item);
                _logger.LogInformation("Item added successfully.");
                return CreatedAtAction(nameof(GetCatalogItemById), new { id = newItem.Id }, newItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding an item.");
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Failed due to {ex}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCatalogItem([FromBody] ItemUpdate item)
        {
            try
            {
                if (item == null)
                {
                    _logger.LogWarning("Item cannot be null");
                    return BadRequest(new { messgage = "Item cannot be null" });
                }
                var res = await _catalogService.UpdateItem(item);
                if (res == null)
                {
                    _logger.LogInformation("Item not found.");
                    return NotFound("Item not found");
                }
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating a catalog item.");
                return StatusCode(500, "Failed to update catalog item due to an unexpected error.");
            }
        }

        [HttpPost]
        [Route("applyCard")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<string>> ApplyCard([FromBody]ApplicationForm form)
        {
            try
            {
                var res = await _applicationService.ApplyCard(form);
                if (res == null)
                {
                    _logger.LogWarning("Failed to apply card.");
                    return BadRequest();
                }
                _logger.LogInformation("Card applied successfully.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to apply card: {ex}");
                return StatusCode(500, "Failed to apply card");
            }
        }

        [HttpPost]
        [Route("updateprice")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof (string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<string>> UpdatePrice([FromBody]ItemPriceChange item)
        {
            try
            {
                var res = await _itemPriceChangeService.PriceChange(item);
                if (res == null)
                {
                    _logger.LogWarning("Price change not effective.");
                    return BadRequest("Price Change not effective");
                }
                _logger.LogInformation("Price change successfull.");
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating the item price.");
                return StatusCode(500, "Failed to update item price due to an unexpected error.");
            }
        }



    }
}
