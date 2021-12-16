using System;
using System.Collections.Generic;
using ERP.Store.Desktop.Repositories;
using ERP.Store.Desktop.Entities.JSON.Request;
using ERP.Store.Desktop.Entities.JSON.Response;

namespace ERP.Store.Desktop.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public List<string> CheckInput(UserRequest request)
        {
            var error = new List<string>();

            try
            {
                if (string.IsNullOrEmpty(request.Username))
                    error.Add("The username cannot be null or empty.");

                if (string.IsNullOrEmpty(request.Password))
                    error.Add("The password cannot be null or empty.");

                return error;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserResponse Login(UserRequest request)
        {
            try
            {
                return _userRepository.Get(request);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
