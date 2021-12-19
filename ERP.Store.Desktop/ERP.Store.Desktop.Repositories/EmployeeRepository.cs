﻿using RestSharp;
using System;
using System.Configuration;
using ERP.Store.Desktop.Entities.JSON.Request;
using ERP.Store.Desktop.Entities.JSON.Response;

namespace ERP.Store.Desktop.Repositories
{
    public class EmployeeRepository
    {
        private readonly string _endpoint;

        public EmployeeRepository()
        {
            _endpoint = ConfigurationManager.ConnectionStrings["EmployeeEndpoint"].ConnectionString;
        }

        public int Post(EmployeeRequest employee, UserResponse user)
        {
            try
            {
                var client = new RestClient(_endpoint)
                {
                    Timeout = -1
                };

                var request = new RestRequest(Method.POST);
                
                request.AddHeader("Authorization", $"Bearer {user.Token.Token}");
                
                request.AddHeader("Content-Type", "application/json");

                #region RequestJSON

                var body = @"{
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

                request.AddParameter("application/json", body, ParameterType.RequestBody);
                
                IRestResponse response = client.Execute(request);

                return (int)response.StatusCode;
            }
            catch (Exception) { throw; }
        }
    }
}