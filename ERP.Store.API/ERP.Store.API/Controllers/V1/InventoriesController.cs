using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ERP.Store.API.CustomExceptions;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Entities.Entities.Enums;
using ERP.Store.API.Services.CustomExceptions;
using ERP.Store.API.Entities.Models.ViewModel.ItemViewModels;
using ERP.Store.API.Entities.Models.InputModel.ItemInputModels;

namespace ERP.Store.API.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly ILogService _logService;

        private readonly IInventoryService _inventoryService;

        private readonly IValidationService _validationService;

        public InventoriesController(ILogService logService, IInventoryService inventoryService, IValidationService validationService)
        {
            _logService = logService;

            _inventoryService = inventoryService;

            _validationService = validationService;
        }

        [HttpGet("{itemID:int}")]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult<ItemDataViewModel>> GetItemAsync([FromRoute] int itemID)
        {
            try
            {
                return await _inventoryService.GetItemAsync(itemID);
            }
            catch (BadRequestException e)
            {
                await _logService.LogAsync(itemID.ToString(), e.Message, "GetItemAsync() : InventoriesController");

                var returnModel = await _validationService.InitializingReturn(e.Message, BadRequest().StatusCode);

                return NotFound(returnModel);
            }
            catch (NotFoundException e)
            {
                await _logService.LogAsync(itemID.ToString(), e.Message, "GetItemAsync() : InventoriesController");

                var returnModel = await _validationService.InitializingReturn(e.Message, NotFound().StatusCode);

                return NotFound(returnModel);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(itemID.ToString(), e.Message, "GetItemAsync() : InventoriesController");

                var returnModel = await _validationService.InitializingReturn(e.Message, 500);

                return StatusCode(500, returnModel);
            }
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<ItemDataViewModel>> RegisterItemAsync([FromBody] ItemDataInputModel model)
        {
            try
            {
                var validations = await _validationService.Validate(model, EntityType.Items);

                if (validations.Any())
                {
                    var returnModel = await _validationService.InitializingReturn(validations, BadRequest().StatusCode);

                    await _logService.LogAsync(returnModel, "Request has errors.", "RegisterItemAsync() : InventoriesController");

                    return BadRequest(returnModel);
                }

                return await _inventoryService.GetItemAsync(await _inventoryService.RegisterItemAsync(model));
            }
            catch (BadRequestException e)
            {
                await _logService.LogAsync(model, e.Message, "RegisterItemAsync() : InventoriesController");

                var returnModel = await _validationService.InitializingReturn(e.Message, BadRequest().StatusCode);

                return NotFound(returnModel);
            }
            catch (NotFoundException e)
            {
                await _logService.LogAsync(model, e.Message, "GetItemAsync() : InventoriesController");

                var returnModel = await _validationService.InitializingReturn(e.Message, NotFound().StatusCode);

                return NotFound(returnModel);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(model, e.Message, "RegisterItemAsync() : InventoriesController");

                var returnModel = await _validationService.InitializingReturn(e.Message, 500);

                return StatusCode(500, returnModel);
            }
        }
    }
}
