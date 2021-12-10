using System;
using System.Threading.Tasks;
using ERP.Store.API.CustomExceptions;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Repositories.Interfaces;
using ERP.Store.API.Entities.Models.ViewModel;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> LoginAsync(UserInputModel userInput)
        {
            try
            {
                var user = await _userRepository.CheckUserAsync(userInput.Username, userInput.Password);

                if (user == null)
                    throw new NotFoundException("Invalid user. Please, check your inputs.");

                return new UserViewModel
                {
                    EmployeeID = user.EmployeeID,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Identification = user.Identification,
                    Role = user.Description,
                    AcessLevelID = user.Acess_LevelID
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
