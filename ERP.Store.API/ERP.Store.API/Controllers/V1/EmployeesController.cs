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
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        //[HttpGet("page={page:int}&quantity={quantity:int}")]
        //public async Task<ActionResult> GetEmployeesAsync([FromQuery] int page, [FromQuery] int quantity)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, $"The following error ocurred: {e.Message}");
        //    }
        //}

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
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"The following error ocurred: {e.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<ActionResult> RegisterEmployeeAsync([FromBody] EmployeeInputModel model)
        {
            try
            {
                await _employeeService.RegisterEmployeeAsync(model);

                return Created("Created", await _employeeService.GetEmployeeAsync(model.Identification));
            }
            catch (ConflictException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"The following error ocurred: {e.Message}");
            }
        }
    }
}
