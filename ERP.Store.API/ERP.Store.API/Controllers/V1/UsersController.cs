using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.Store.API.CustomExceptions;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Controllers.V1
{
    [ApiController]
    [Route("api/V1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly ITokenService _tokenService;

        public UsersController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;

            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] UserInputModel userInput)
        {
            try
            {
                var user = await _userService.LoginAsync(userInput);

                var token = _tokenService.GenerateToken(user);

                return new
                {
                    user.EmployeeID,
                    user.FirstName,
                    user.MiddleName,
                    user.LastName,
                    Token = new
                    {
                        token
                    }
                };
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
    }
}
