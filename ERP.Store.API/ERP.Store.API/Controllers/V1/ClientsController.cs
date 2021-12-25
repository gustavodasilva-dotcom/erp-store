using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ERP.Store.API.CustomExceptions;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Services.CustomExceptions;
using ERP.Store.API.Entities.Models.ViewModel;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ILogService _logService;

        private readonly IClientService _clientService;

        public ClientsController(ILogService logService, IClientService clientService)
        {
            _logService = logService;

            _clientService = clientService;
        }

        [HttpGet("{identification}")]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult<ClientViewModel>> GetClientAsync([FromRoute] string identification)
        {
            try
            {
                return (await _clientService.GetClientAsync(identification));
            }
            catch (NotFoundException e)
            {
                await _logService.LogAsync(identification, e.Message, "GetClientAsync() : ClientsController");

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(identification, e.Message, "GetClientAsync() : ClientsController");

                return StatusCode(500, $"The following error occurred: {e.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult> RegisterClientAsync([FromBody] ClientInputModel model)
        {
            try
            {
                await _clientService.RegisterClientAsync(model);

                var client = await _clientService.GetClientAsync(model.Identification);

                await _logService.LogAsync(model, "Client registered successfully.", "RegisterClientAsync() : ClientsController", client.ID);

                return Created("Created", client);
            }
            catch (ConflictException e)
            {
                await _logService.LogAsync(model, e.Message, "RegisterClientAsync() : ClientsController");

                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(model, e.Message, "RegisterClientAsync() : ClientsController");

                return StatusCode(500, $"The following error occurred: {e.Message}");
            }
        }

        [HttpPut]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult<ClientViewModel>> UpdateEmployeeAsync([FromBody] ClientInputModel model)
        {
            try
            {
                await _clientService.UpdateClientAsync(model);

                var client = await _clientService.GetClientAsync(model.Identification);

                await _logService.LogAsync(model, "Client updated successfully.", "UpdateEmployeeAsync() : ClientsController", client.ID);

                return Ok(client);
            }
            catch (NotFoundException e)
            {
                await _logService.LogAsync(model, e.Message, "UpdateEmployeeAsync() : ClientsController");

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(model, e.Message, "UpdateEmployeeAsync() : ClientsController");

                return StatusCode(500, $"The following error occurred: {e.Message}");
            }
        }
    }
}
