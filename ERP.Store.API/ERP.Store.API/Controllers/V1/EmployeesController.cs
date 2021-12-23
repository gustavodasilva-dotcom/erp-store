using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.Store.API.CustomExceptions;
using ERP.Store.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        public EmployeesController(ILogService logService, IEmployeeService employeeService)
        {
            _logService = logService;

            _employeeService = employeeService;
        }

        [HttpGet("{identification}")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployeeAsync([FromRoute] string identification)
        {
            try
            {
                return (await _employeeService.GetEmployeeAsync(identification));
            }
            catch (NotFoundException e)
            {
                await _logService.LogAsync(identification, e.Message, "GetEmployeeAsync() : EmployeesController");

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(identification, e.Message, "GetEmployeeAsync() : EmployeesController");

                return StatusCode(500, $"The following error occurred: {e.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<ActionResult> RegisterEmployeeAsync([FromBody] EmployeeInputModel model)
        {
            try
            {
                await _employeeService.RegisterEmployeeAsync(model);

                var employee = await _employeeService.GetEmployeeAsync(model.Identification);

                await _logService.LogAsync(model, "Employee registered successfully.", "RegisterEmployeeAsync() : EmployeesController", employee.ID);

                return Created("Created", employee);
            }
            catch (ConflictException e)
            {
                await _logService.LogAsync(model, e.Message, "RegisterEmployeeAsync() : EmployeesController");

                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(model, e.Message, "RegisterEmployeeAsync() : EmployeesController");

                return StatusCode(500, $"The following error occurred: {e.Message}");
            }
        }

        [HttpPut]
        [Authorize(Roles = "1")]
        public async Task<ActionResult> UpdateEmployeeAsync([FromBody] EmployeeInputModel model)
        {
            try
            {
                await _employeeService.UpdateEmployeeAsync(model);

                var employee = await _employeeService.GetEmployeeAsync(model.Identification);

                await _logService.LogAsync(model, "Employee updated successfully.", "UpdateEmployeeAsync() : EmployeesController", employee.ID);

                return Ok(employee);
            }
            catch (NotFoundException e)
            {
                await _logService.LogAsync(model, e.Message, "UpdateEmployeeAsync() : EmployeesController");

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(model, e.Message, "UpdateEmployeeAsync() : EmployeesController");

                return StatusCode(500, $"The following error occurred: {e.Message}");
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

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(identification, e.Message, "DeleteEmployeeAsync() : EmployeesController");

                return StatusCode(500, $"The following error occurred: {e.Message}");
            }
        }
    }
}
