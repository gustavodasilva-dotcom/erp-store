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
        private readonly ILogService _logService;

        private readonly IUserService _userService;

        private readonly ITokenService _tokenService;

        public UsersController(ILogService logService, IUserService userService, ITokenService tokenService)
        {
            _logService = logService;

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

                await _logService.LogAsync(userInput, "Employee logged successfully.", "AuthenticateAsync() : UsersController", token, user.EmployeeID);

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
                await _logService.LogAsync(userInput, e.Message, "AuthenticateAsync() : UsersController");

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(userInput, e.Message, "AuthenticateAsync() : UsersController");

                return StatusCode(500, $"The following error occurred: {e.Message}");
            }
        }
    }
}
