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
    public class SuppliersController : ControllerBase
    {
        private readonly ILogService _logService;

        private readonly ISupplierService _supplierService;

        public SuppliersController(ILogService logService, ISupplierService supplierService)
        {
            _logService = logService;

            _supplierService = supplierService;
        }

        [HttpGet("{identification}")]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult<SupplierViewModel>> GetSupplierAsync([FromRoute] string identification)
        {
            try
            {
                return await _supplierService.GetSupplierAsync(identification);
            }
            catch (NotFoundException e)
            {
                await _logService.LogAsync(identification, e.Message, "GetSupplierAsync() : SuppliersController");

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(identification, e.Message, "GetSupplierAsync() : SuppliersController");

                return StatusCode(500, $"The following error occurred: {e.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<ActionResult> RegisterSupplierAsync([FromBody] SupplierInputModel model)
        {
            try
            {
                await _supplierService.RegisterSupplierAsync(model);

                var supplier = await _supplierService.GetSupplierAsync(model.Identification);

                await _logService.LogAsync(model, "Client registered successfully.", "RegisterSupplierAsync() : SuppliersController", supplier.ID);

                return Created("Created", supplier);
            }
            catch (ConflictException e)
            {
                await _logService.LogAsync(model, e.Message, "RegisterSupplierAsync() : SuppliersController");

                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(model, e.Message, "RegisterSupplierAsync() : SuppliersController");

                return StatusCode(500, $"The following error occurred: {e.Message}");
            }
        }

        [HttpPut]
        [Authorize(Roles = "1")]
        public async Task<ActionResult> UpdateSupplierAsync([FromBody] SupplierInputModel model)
        {
            try
            {
                await _supplierService.UpdateSupplierAsync(model);

                var supplier = await _supplierService.GetSupplierAsync(model.Identification);

                await _logService.LogAsync(model, "Client updated successfully.", "UpdateSupplierAsync() : SuppliersController", supplier.ID);

                return Ok(supplier);
            }
            catch (NotFoundException e)
            {
                await _logService.LogAsync(model, e.Message, "UpdateSupplierAsync() : SuppliersController");

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(model, e.Message, "UpdateSupplierAsync() : SuppliersController");

                return StatusCode(500, $"The following error occurred: {e.Message}");
            }
        }

        [HttpDelete("{identification}")]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult> DeleteSupplierAsync([FromRoute] string identification)
        {
            try
            {
                if (await _supplierService.DeleteSupplierAsync(identification))
                {
                    await _logService.LogAsync(identification, "Supplier deleted successfully.", "DeleteSupplierAsync() : SuppliersController");

                    return Ok("Supplier deleted successfully.");
                }
                else
                {
                    throw new Exception("An error occurred while trying to delete the client.");
                }
            }
            catch (NotFoundException e)
            {
                await _logService.LogAsync(identification, e.Message, "DeleteSupplierAsync() : SuppliersController");

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(identification, e.Message, "DeleteSupplierAsync() : SuppliersController");

                return StatusCode(500, $"The following error occurred: {e.Message}");
            }
        }
    }
}
