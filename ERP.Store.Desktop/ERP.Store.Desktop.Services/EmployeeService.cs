using System;
using System.Configuration;
using ERP.Store.Desktop.Repositories;
using ERP.Store.Desktop.Entities.JSON.Request;

namespace ERP.Store.Desktop.Services
{
    public class EmployeeService
    {
        private APIRepository _apiRepository { get; set; }

        public EmployeeService()
        {
            _apiRepository = new APIRepository();
        }

        public int Post(EmployeeRequest employee, dynamic user)
        {
            try
            {
                var json = CreateJson(employee);

                _apiRepository.Endpoint = ConfigurationManager.ConnectionStrings["EmployeeEndpoint"].ConnectionString;

                var response = _apiRepository.Post(json, user);

                if (response == null) throw new Exception("It was not possible to complete the request.");

                return response.id;
            }
            catch (Exception) { throw; }
        }

        public int Put(EmployeeRequest employee, dynamic user)
        {
            try
            {
                var json = CreateJson(employee);

                _apiRepository.Endpoint = ConfigurationManager.ConnectionStrings["EmployeeEndpoint"].ConnectionString;

                var response = _apiRepository.Put(json, user);

                if (response == null) throw new Exception("It was not possible to complete the request.");

                return response.id;
            }
            catch (Exception) { throw; }
        }

        public dynamic Get(string identification, dynamic user)
        {
            try
            {
                _apiRepository.Endpoint = ConfigurationManager.ConnectionStrings["EmployeeEndpoint"].ConnectionString + identification;

                var response = _apiRepository.Get(user);

                if (response == null) throw new Exception("It was not possible to complete the request.");

                return response;
            }
            catch (Exception) { throw; }
        }

        public dynamic Delete(string identification, dynamic user)
        {
            try
            {
                _apiRepository.Endpoint = ConfigurationManager.ConnectionStrings["EmployeeEndpoint"].ConnectionString + identification;

                var response = _apiRepository.Delete(user);

                if (response == null) throw new Exception("It was not possible to complete the request.");

                return response;
            }
            catch (Exception) { throw; }
        }

        private string CreateJson(EmployeeRequest employee)
        {
            try
            {
                #region CreateJson

                return
                @"{
                    " + "\n" +
                                    $@"  ""firstName"": ""{employee.FirstName}"",
                    " + "\n" +
                                    $@"  ""middleName"": ""{employee.MiddleName}"",
                    " + "\n" +
                                    $@"  ""lastName"": ""{employee.LastName}"",
                    " + "\n" +
                                    $@"  ""identification"": ""{employee.Identification}"",
                    " + "\n" +
                                    @"  ""address"": {
                    " + "\n" +
                                    $@"    ""zip"": ""{employee.Address.Zip}"",
                    " + "\n" +
                                    $@"    ""street"": ""{employee.Address.Street}"",
                    " + "\n" +
                                    $@"    ""number"": ""{employee.Address.Number}"",
                    " + "\n" +
                                    $@"    ""comment"": ""{employee.Address.Comment}"",
                    " + "\n" +
                                    $@"    ""neighborhood"": ""{employee.Address.Neighborhood}"",
                    " + "\n" +
                                    $@"    ""city"": ""{employee.Address.City}"",
                    " + "\n" +
                                    $@"    ""state"": ""{employee.Address.State}"",
                    " + "\n" +
                                    $@"    ""country"": ""{employee.Address.Country}""
                    " + "\n" +
                                    @"  },
                    " + "\n" +
                                    @"  ""userInfo"": {
                    " + "\n" +
                                    $@"    ""username"": ""{employee.UserInfo.Username}"",
                    " + "\n" +
                                    $@"    ""password"": ""{employee.UserInfo.Password}""
                    " + "\n" +
                                    @"  },
                    " + "\n" +
                                    @"  ""contact"": {
                    " + "\n" +
                                    $@"    ""email"": ""{employee.Contact.Email}"",
                    " + "\n" +
                                    $@"    ""cellphone"": ""{employee.Contact.Cellphone}"",
                    " + "\n" +
                                    $@"    ""phone"": ""{employee.Contact.Phone}""
                    " + "\n" +
                                    @"  },
                    " + "\n" +
                                    @"  ""extraInfo"": {
                    " + "\n" +
                                    $@"    ""accessLevelID"": {employee.ExtraInfo.AccessLevelID},
                    " + "\n" +
                                    $@"    ""salary"": {employee.ExtraInfo.Salary},
                    " + "\n" +
                                    $@"    ""jobID"": {employee.ExtraInfo.JobID}
                    " + "\n" +
                                    @"  },
                    " + "\n" +
                                    @"  ""image"": {
                    " + "\n" +
                                    $@"    ""isImage"": {employee.Image.IsImage.ToString().ToLower()},
                    " + "\n" +
                                    $@"    ""base64"": ""{employee.Image.Base64}""
                    " + "\n" +
                                    @"  }
                    " + "\n" +
                @"}";

                #endregion
            }
            catch (Exception) { throw; }
        }
    }
}
