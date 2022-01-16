using System;
using RestSharp;
using Newtonsoft.Json;
using System.Configuration;
using ERP.Store.Desktop.Entities.JSON.Request;

namespace ERP.Store.Desktop.Repositories
{
    public class UserRepository
    {
        private readonly string _endpoint;

        public UserRepository()
        {
            _endpoint = ConfigurationManager.ConnectionStrings["UserEndpoint"].ConnectionString;
        }

        public dynamic Get(UserRequest user)
        {
            try
            {
                var client = new RestClient(_endpoint)
                {
                    Timeout = -1
                };

                var request = new RestRequest(Method.POST);
                
                request.AddHeader("Content-Type", "application/json");
                
                var body =
                @"{
                    " + "\n" +
                                    $@"  ""username"": ""{user.Username}"",
                    " + "\n" +
                                    $@"  ""password"": ""{user.Password}""
                    " + "\n" +
                @"}";

                request.AddParameter("application/json", body, ParameterType.RequestBody);
                
                IRestResponse response = client.Execute(request);

                if ((int)response.StatusCode == 404)
                    throw new Exception("No user found. Please, check your input data.");

                if ((int)response.StatusCode == 500 || response.StatusCode == 0)
                    throw new Exception("An error ocurred while connectiong to the server.");

                return JsonConvert.DeserializeObject<dynamic>(response.Content);
            }
            catch (Exception) { throw; }
        }
    }
}
