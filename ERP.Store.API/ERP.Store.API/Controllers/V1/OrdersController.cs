using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ERP.Store.API.CustomExceptions;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogService _logService;

        private readonly IOrderService _orderService;

        private readonly IValidationService _validationService;

        public OrdersController(ILogService logService, IOrderService orderService, IValidationService validationService)
        {
            _logService = logService;

            _orderService = orderService;

            _validationService = validationService;
        }

        [HttpGet("{orderID:int}")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<dynamic>> GetOrderAsync([FromRoute] int orderID)
        {
            try
            {
                return Ok(await _orderService.GetOrderAsync(orderID));
            }
            catch (NotFoundException e)
            {
                await _logService.LogAsync(orderID, e.Message, "GetOrderAsync() : OrdersController");

                var returnModel = await _validationService.InitializingReturn(e.Message, NotFound().StatusCode);

                return NotFound(returnModel);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(orderID, e.Message, "GetOrderAsync() : OrdersController");

                var returnModel = await _validationService.InitializingReturn(e.Message, 500);

                return StatusCode(500, returnModel);
            }
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<ActionResult> RegisterOrderAsync([FromBody] OrderInputModel model)
        {
            try
            {
                await _orderService.RegisterOrderAsync(model);

                return Ok();
            }
            catch (NotFoundException e)
            {
                await _logService.LogAsync(model, e.Message, "RegisterOrderAsync() : OrdersController");

                var returnModel = await _validationService.InitializingReturn(e.Message, NotFound().StatusCode);

                return NotFound(returnModel);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(model, e.Message, "RegisterOrderAsync() : OrdersController");

                var returnModel = await _validationService.InitializingReturn(e.Message, 500);

                return StatusCode(500, returnModel);
            }
        }
    }
}
