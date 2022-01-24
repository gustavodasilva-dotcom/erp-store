using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ERP.Store.API.CustomExceptions;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Entities.Entities.Enums;
using ERP.Store.API.Entities.Models.ViewModel;
using ERP.Store.API.Services.CustomExceptions;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogService _logService;

        private readonly IEmployeeService _employeeService;

        private readonly IValidationService _validationService;

        public EmployeesController(ILogService logService, IEmployeeService employeeService, IValidationService validationService)
        {
            _logService = logService;

            _employeeService = employeeService;

            _validationService = validationService;
        }

        [HttpGet("{identification}")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployeeAsync([FromRoute] string identification)
        {
            try
            {
                return await _employeeService.GetEmployeeAsync(identification);
            }
            catch (NotFoundException e)
            {
                await _logService.LogAsync(identification, e.Message, "GetEmployeeAsync() : EmployeesController");

                var returnModel = await _validationService.InitializingReturn(e.Message, NotFound().StatusCode);

                return NotFound(returnModel);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(identification, e.Message, "GetEmployeeAsync() : EmployeesController");

                var returnModel = await _validationService.InitializingReturn(e.Message, 500);

                return StatusCode(500, returnModel);
            }
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<ActionResult> RegisterEmployeeAsync([FromBody] EmployeeInputModel model)
        {
            try
            {
                var validations = await _validationService.Validate(model, EntityType.Employees);

                if (validations.Any())
                {
                    var returnModel = await _validationService.InitializingReturn(validations, BadRequest().StatusCode);

                    await _logService.LogAsync(returnModel, "Request has errors.", "RegisterEmployeeAsync() : EmployeesController");

                    return BadRequest(returnModel);
                }

                await _employeeService.RegisterEmployeeAsync(model);

                var employee = await _employeeService.GetEmployeeAsync(model.Identification);

                await _logService.LogAsync(model, "Employee registered successfully.", "RegisterEmployeeAsync() : EmployeesController", employee.ID);

                return Created(string.Empty, employee);
            }
            catch (ConflictException e)
            {
                await _logService.LogAsync(model, e.Message, "RegisterEmployeeAsync() : EmployeesController");

                var returnModel = await _validationService.InitializingReturn(e.Message, Conflict().StatusCode);

                return Conflict(returnModel);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(model, e.Message, "RegisterEmployeeAsync() : EmployeesController");

                var returnModel = await _validationService.InitializingReturn(e.Message, 500);

                return StatusCode(500, returnModel);
            }
        }

        [HttpPut]
        [Authorize(Roles = "1")]
        public async Task<ActionResult> UpdateEmployeeAsync([FromBody] EmployeeInputModel model)
        {
            try
            {
                var validations = await _validationService.Validate(model, EntityType.Employees);

                if (validations.Any())
                {
                    var returnModel = await _validationService.InitializingReturn(validations, BadRequest().StatusCode);

                    await _logService.LogAsync(returnModel, "Request has errors.", "UpdateEmployeeAsync() : EmployeesController");

                    return BadRequest(returnModel);
                }

                await _employeeService.UpdateEmployeeAsync(model);

                var employee = await _employeeService.GetEmployeeAsync(model.Identification);

                await _logService.LogAsync(model, "Employee updated successfully.", "UpdateEmployeeAsync() : EmployeesController", employee.ID);

                return Ok(employee);
            }
            catch (NotFoundException e)
            {
                await _logService.LogAsync(model, e.Message, "UpdateEmployeeAsync() : EmployeesController");

                var returnModel = await _validationService.InitializingReturn(e.Message, NotFound().StatusCode);

                return NotFound(returnModel);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(model, e.Message, "UpdateEmployeeAsync() : EmployeesController");

                var returnModel = await _validationService.InitializingReturn(e.Message, 500);

                return StatusCode(500, returnModel);
            }
        }

        [HttpDelete("{identification}")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult> DeleteEmployeeAsync([FromRoute] string identification)
        {
            try
            {
                if (await _employeeService.DeleteEmployeeAsync(identification))
                {
                    await _logService.LogAsync(identification, "Employee deleted successfully.", "DeleteEmployeeAsync() : EmployeesController");

                    return Ok("Employee deleted successfully.");
                }
                else
                {
                    throw new Exception("An error occurred while trying to delete the employee.");
                }
            }
            catch (NotFoundException e)
            {
                await _logService.LogAsync(identification, e.Message, "DeleteEmployeeAsync() : EmployeesController");

                var returnModel = await _validationService.InitializingReturn(e.Message, NotFound().StatusCode);

                return NotFound(returnModel);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(identification, e.Message, "DeleteEmployeeAsync() : EmployeesController");

                var returnModel = await _validationService.InitializingReturn(e.Message, 500);

                return StatusCode(500, returnModel);
            }
        }
    }
}
