using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.Store.API.CustomExceptions;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Entities.Entities.Enums;
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

        private readonly IValidationService _validationService;

        public UsersController(ILogService logService, IUserService userService, ITokenService tokenService, IValidationService validationService)
        {
            _logService = logService;

            _userService = userService;

            _tokenService = tokenService;

            _validationService = validationService;
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] UserInputModel userInput)
        {
            try
            {
                var validations = await _validationService.Validate(userInput, EntityType.Users);

                if (validations.Any())
                {
                    var returnModel = await _validationService.InitializingReturn(validations, BadRequest().StatusCode);

                    await _logService.LogAsync(returnModel, "Request has errors.", "AuthenticateAsync() : UsersController");

                    return BadRequest(returnModel);
                }

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

                var returnModel = await _validationService.InitializingReturn(e.Message, NotFound().StatusCode);

                return NotFound(returnModel);
            }
            catch (Exception e)
            {
                await _logService.LogAsync(userInput, e.Message, "AuthenticateAsync() : UsersController");

                var returnModel = await _validationService.InitializingReturn(e.Message, 500);

                return StatusCode(500, returnModel);
            }
        }
    }
}
