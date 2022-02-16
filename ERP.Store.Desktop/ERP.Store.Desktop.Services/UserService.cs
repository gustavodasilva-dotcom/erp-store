using System;
using System.Configuration;
using System.Collections.Generic;
using ERP.Store.Desktop.Repositories;
using ERP.Store.Desktop.Entities.JSON.Request;

namespace ERP.Store.Desktop.Services
{
    public class UserService
    {
        private APIRepository _apiRepository { get; set; }

        public UserService()
        {
            _apiRepository = new APIRepository();
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

        public dynamic Login(UserRequest request)
        {
            try
            {
                var json = CreateJson(request);

                _apiRepository.Endpoint = ConfigurationManager.ConnectionStrings["UserEndpoint"].ConnectionString;

                var response = _apiRepository.Post(json);

                if (response == null) throw new Exception("It was not possible to complete de request.");

                return response;
            }
            catch (Exception) { throw; }
        }

        private string CreateJson(UserRequest request)
        {
            try
            {
                return
                @"{
                    " + "\n" +
                                    $@"  ""username"": ""{request.Username}"",
                    " + "\n" +
                                    $@"  ""password"": ""{request.Password}""
                    " + "\n" +
                @"}";
            }
            catch (Exception) { throw; }
        }
    }
}
