using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.Store.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<ActionResult> RegisterEmployeeAsync([FromBody] EmployeeInputModel model)
        {
            try
            {
                await _employeeService.RegisterEmployeeAsync(model);

                //TODO: to implement the return of the registered employee.

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"The following error ocurred: {e.Message}");
            }
        }
    }
}
