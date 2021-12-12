using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ERP.Store.API.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //[HttpPost]
        //[Authorize(Roles = "1")]
        //public async Task<ActionResult> RegisterEmployeeAsync()
        //{
        //    try
        //    {

        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, $"The following error ocurred: {e.Message}");
        //    }
        //}
    }
}
