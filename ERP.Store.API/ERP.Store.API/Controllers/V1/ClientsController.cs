using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ERP.Store.API.CustomExceptions;
using ERP.Store.API.Entities.Entities;
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

        private readonly IValidationService _validationService;

        public ClientsController(ILogService logService, IClientService clientService, IValidationService validationService)
        {
            _logService = logService;

            _clientService = clientService;

            _validationService = validationService;
        }

        [HttpGet("{identification}")]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult<ClientViewModel>> GetClientAsync([FromRoute] string identification)
        {
            try
            {
                return await _clientService.GetClientAsync(identification);
            }
            catch (NotFoundException e)
            {
                await _logService.LogAsync(identification, e.Message, "GetClientAsync() : ClientsController");

                var returnModel = await _validationService.InitializingReturn(e.Message, NotFound().StatusCode);

                return NotFound(returnModel);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(identification, e.Message, "GetClientAsync() : ClientsController");

                var returnModel = await _validationService.InitializingReturn(e.Message, 500);

                return StatusCode(500, returnModel);
            }
        }

        [HttpPost]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult> RegisterClientAsync([FromBody] ClientInputModel model)
        {
            try
            {
                var validations = await _validationService.Validate(model, EntityType.Clients);

                if (validations.Any())
                {
                    var returnModel = await _validationService.InitializingReturn(validations, BadRequest().StatusCode);

                    await _logService.LogAsync(returnModel, "Request has errors.", "RegisterClientAsync() : ClientsController");

                    return BadRequest(returnModel);
                }

                await _clientService.RegisterClientAsync(model);

                var client = await _clientService.GetClientAsync(model.Identification);

                await _logService.LogAsync(model, "Client registered successfully.", "RegisterClientAsync() : ClientsController", client.ID);

                return Created("Created", client);
            }
            catch (ConflictException e)
            {
                await _logService.LogAsync(model, e.Message, "RegisterClientAsync() : ClientsController");

                var returnModel = await _validationService.InitializingReturn(e.Message, Conflict().StatusCode);

                return Conflict(returnModel);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(model, e.Message, "RegisterClientAsync() : ClientsController");

                var returnModel = await _validationService.InitializingReturn(e.Message, 500);

                return StatusCode(500, returnModel);
            }
        }

        [HttpPut]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult<ClientViewModel>> UpdateClientAsync([FromBody] ClientInputModel model)
        {
            try
            {
                var validations = await _validationService.Validate(model, EntityType.Clients);

                if (validations.Any())
                {
                    var returnModel = await _validationService.InitializingReturn(validations, BadRequest().StatusCode);

                    await _logService.LogAsync(returnModel, "Request has errors.", "UpdateClientAsync() : ClientsController");

                    return BadRequest(returnModel);
                }

                await _clientService.UpdateClientAsync(model);

                var client = await _clientService.GetClientAsync(model.Identification);

                await _logService.LogAsync(model, "Client updated successfully.", "UpdateClientAsync() : ClientsController", client.ID);

                return Ok(client);
            }
            catch (NotFoundException e)
            {
                await _logService.LogAsync(model, e.Message, "UpdateClientAsync() : ClientsController");

                var returnModel = await _validationService.InitializingReturn(e.Message, NotFound().StatusCode);

                return NotFound(returnModel);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(model, e.Message, "UpdateClientAsync() : ClientsController");

                var returnModel = await _validationService.InitializingReturn(e.Message, 500);

                return StatusCode(500, returnModel);
            }
        }

        [HttpDelete("{identification}")]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult> DeleteClientAsync([FromRoute] string identification)
        {
            try
            {
                if (await _clientService.DeleteClientAsync(identification))
                {
                    await _logService.LogAsync(identification, "Employee client successfully.", "DeleteClientAsync() : ClientsController");

                    return Ok("Client deleted successfully.");
                }
                else
                {
                    throw new Exception("An error occurred while trying to delete the client.");
                }
            }
            catch (NotFoundException e)
            {
                await _logService.LogAsync(identification, e.Message, "DeleteClientAsync() : ClientsController");

                var returnModel = await _validationService.InitializingReturn(e.Message, NotFound().StatusCode);

                return NotFound(returnModel);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(identification, e.Message, "DeleteClientAsync() : ClientsController");
                
                var returnModel = await _validationService.InitializingReturn(e.Message, 500);

                return StatusCode(500, returnModel);
            }
        }
    }
}
