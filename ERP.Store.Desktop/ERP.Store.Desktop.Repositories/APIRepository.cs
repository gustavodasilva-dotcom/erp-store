using RestSharp;
using System;
using System.Configuration;
using ERP.Store.Desktop.Entities.Entities;
using Newtonsoft.Json;

namespace ERP.Store.Desktop.Repositories
{
    public class APIRepository
    {
        public string Endpoint { get; set; }

        public dynamic Post(string json, dynamic user, HttpMethodType httpMethod)
        {
            try
            {
                var client = new RestClient(Endpoint)
                {
                    Timeout = -1
                };

                var request = new RestRequest
                {
                    Method = httpMethod switch
                    {
                        HttpMethodType.Get => Method.GET,
                        HttpMethodType.Post => Method.POST,
                        HttpMethodType.Put => Method.PUT,
                        HttpMethodType.Patch => Method.PATCH,
                        HttpMethodType.Delete => Method.DELETE,
                        _ => throw new ArgumentOutOfRangeException("A HTTP Method needs to be informed, so that the request must be completed."),
                    }
                };

                request.AddHeader("Authorization", $"Bearer {user.token.token}");

                request.AddHeader("Content-Type", "application/json");

                request.AddParameter("application/json", json, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                if ((int)response.StatusCode != 200)
                {
                    var message = string.Empty;

                    message = (int)response.StatusCode switch
                    {
                        400 => $"Invalid request. Please, check your input data.",
                        401 => $"This user doens't have authorization to complete this request.",
                        404 => $"No data found. Please, check your input data.",
                        _ => "An error occurred while processing the request.",
                    };

                    throw new Exception(message);
                }

                return JsonConvert.DeserializeObject<dynamic>(response.Content);
            }
            catch (Exception) { throw; }
        }
    }
}
