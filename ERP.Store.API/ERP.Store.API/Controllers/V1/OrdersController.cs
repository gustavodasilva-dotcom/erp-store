using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ERP.Store.API.CustomExceptions;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Entities.Models.InputModel;
using ERP.Store.API.Services.CustomExceptions;

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
        [Authorize(Roles = "1,2")]
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

        [HttpPost("{orderID:int}")]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult<dynamic>> CompleteOrCancelOrderAsync([FromRoute] int orderID, [FromBody] CompleteOrderInputModel model)
        {
            try
            {
                if (orderID != model.OrderID) throw new BadRequestException("The order ID at the route and the order ID at the body must be the same.");

                await _logService.LogAsync(model, "Requesting completion or cancelation of order.", "CompleteOrderAsync() : OrdersController", model.OrderID);

                await _orderService.CompleteOrCancelOrderAsync(model);

                await _logService.LogAsync(model, $"Order {model.OrderID} completed or canceled.", "CompleteOrderAsync() : OrdersController", model.OrderID);

                return Ok(await _orderService.GetOrderAsync(model.OrderID));
            }
            catch (BadRequestException e)
            {
                await _logService.LogAsync(model, e.Message, "CompleteOrderAsync() : InventoriesController");

                var returnModel = await _validationService.InitializingReturn(e.Message, BadRequest().StatusCode);

                return BadRequest(returnModel);
            }
            catch (NotFoundException e)
            {
                await _logService.LogAsync(model, e.Message, "CompleteOrderAsync() : OrdersController");

                var returnModel = await _validationService.InitializingReturn(e.Message, NotFound().StatusCode);

                return NotFound(returnModel);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(model, e.Message, "CompleteOrderAsync() : OrdersController");

                var returnModel = await _validationService.InitializingReturn(e.Message, 500);

                return StatusCode(500, returnModel);
            }
        }

        [HttpPost]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult<dynamic>> RegisterOrderAsync([FromBody] OrderInputModel model)
        {
            try
            {
                return Created(string.Empty, await _orderService.GetOrderAsync(await _orderService.RegisterOrderAsync(model)));
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

        [HttpPut("{orderID:int}")]
        //[Authorize(Roles = "1,2")]
        public async Task UpdateOrderAsync([FromBody] OrderInputModel model, [FromRoute] int orderID)
        {
            try
            {
                await _orderService.UpdateOrderAsync(model, orderID);
            }
            catch (Exception) { }
        }
    }
}
