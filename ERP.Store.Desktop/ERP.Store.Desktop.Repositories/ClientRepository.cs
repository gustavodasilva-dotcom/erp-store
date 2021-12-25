using RestSharp;
using System;
using System.Configuration;
using ERP.Store.Desktop.Entities.JSON.Request;
using ERP.Store.Desktop.Entities.JSON.Response;
using Newtonsoft.Json;

namespace ERP.Store.Desktop.Repositories
{
    public class ClientRepository
    {
        private readonly string _endpoint;

        public ClientRepository()
        {
            _endpoint = ConfigurationManager.ConnectionStrings["ClientEndpoint"].ConnectionString;
        }

        public int Post(ClientRequest clientRequest, UserResponse user)
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
                                    $@"  ""firstName"": ""{clientRequest.FirstName}"",
                    " + "\n" +
                                    $@"  ""middleName"": ""{clientRequest.MiddleName}"",
                    " + "\n" +
                                    $@"  ""lastName"": ""{clientRequest.LastName}"",
                    " + "\n" +
                                    $@"  ""identification"": ""{clientRequest.Identification}"",
                    " + "\n" +
                                    @"  ""address"": {
                    " + "\n" +
                                    $@"    ""zip"": ""{clientRequest.Address.Zip}"",
                    " + "\n" +
                                    $@"    ""street"": ""{clientRequest.Address.Street}"",
                    " + "\n" +
                                    $@"    ""number"": ""{clientRequest.Address.Number}"",
                    " + "\n" +
                                    $@"    ""comment"": ""{clientRequest.Address.Comment}"",
                    " + "\n" +
                                    $@"    ""neighborhood"": ""{clientRequest.Address.Neighborhood}"",
                    " + "\n" +
                                    $@"    ""city"": ""{clientRequest.Address.City}"",
                    " + "\n" +
                                    $@"    ""state"": ""{clientRequest.Address.State}"",
                    " + "\n" +
                                    $@"    ""country"": ""{clientRequest.Address.Country}""
                    " + "\n" +
                                    @"  },
                    " + "\n" +
                                    @"  ""contact"": {
                    " + "\n" +
                                    $@"    ""email"": ""{clientRequest.Contact.Email}"",
                    " + "\n" +
                                    $@"    ""cellphone"": ""{clientRequest.Contact.Cellphone}"",
                    " + "\n" +
                                    $@"    ""phone"": ""{clientRequest.Contact.Phone}""
                    " + "\n" +
                                    @"  },
                    " + "\n" +
                                    @"  ""image"": {
                    " + "\n" +
                                    $@"    ""isImage"": {clientRequest.Image.IsImage.ToString().ToLower()},
                    " + "\n" +
                                    $@"    ""base64"": ""{clientRequest.Image.Base64}""
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

        public int Put(ClientRequest clientRequest, UserResponse user)
        {
            try
            {
                var client = new RestClient(_endpoint)
                {
                    Timeout = -1
                };

                var request = new RestRequest(Method.PUT);

                request.AddHeader("Authorization", $"Bearer {user.Token.Token}");

                request.AddHeader("Content-Type", "application/json");

                #region RequestJSON

                var body = @"{
                    " + "\n" +
                                    $@"  ""firstName"": ""{clientRequest.FirstName}"",
                    " + "\n" +
                                    $@"  ""middleName"": ""{clientRequest.MiddleName}"",
                    " + "\n" +
                                    $@"  ""lastName"": ""{clientRequest.LastName}"",
                    " + "\n" +
                                    $@"  ""identification"": ""{clientRequest.Identification}"",
                    " + "\n" +
                                    @"  ""address"": {
                    " + "\n" +
                                    $@"    ""zip"": ""{clientRequest.Address.Zip}"",
                    " + "\n" +
                                    $@"    ""street"": ""{clientRequest.Address.Street}"",
                    " + "\n" +
                                    $@"    ""number"": ""{clientRequest.Address.Number}"",
                    " + "\n" +
                                    $@"    ""comment"": ""{clientRequest.Address.Comment}"",
                    " + "\n" +
                                    $@"    ""neighborhood"": ""{clientRequest.Address.Neighborhood}"",
                    " + "\n" +
                                    $@"    ""city"": ""{clientRequest.Address.City}"",
                    " + "\n" +
                                    $@"    ""state"": ""{clientRequest.Address.State}"",
                    " + "\n" +
                                    $@"    ""country"": ""{clientRequest.Address.Country}""
                    " + "\n" +
                                    @"  },
                    " + "\n" +
                                    @"  ""contact"": {
                    " + "\n" +
                                    $@"    ""email"": ""{clientRequest.Contact.Email}"",
                    " + "\n" +
                                    $@"    ""cellphone"": ""{clientRequest.Contact.Cellphone}"",
                    " + "\n" +
                                    $@"    ""phone"": ""{clientRequest.Contact.Phone}""
                    " + "\n" +
                                    @"  },
                    " + "\n" +
                                    @"  ""image"": {
                    " + "\n" +
                                    $@"    ""isImage"": {clientRequest.Image.IsImage.ToString().ToLower()},
                    " + "\n" +
                                    $@"    ""base64"": ""{clientRequest.Image.Base64}""
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

        public ClientResponse Get(string identification, UserResponse user)
        {
            try
            {
                var client = new RestClient(_endpoint + identification)
                {
                    Timeout = -1
                };

                var request = new RestRequest(Method.GET);

                request.AddHeader("Authorization", $"Bearer {user.Token.Token}");

                request.AddHeader("Content-Type", "application/json");

                request.AddParameter("application/json", ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                if ((int)response.StatusCode != 200)
                {
                    var message = string.Empty;

                    switch ((int)response.StatusCode)
                    {
                        case 400:
                            message = $"Invalid request. Please, check your input data.";
                            break;
                        case 401:
                            message = $"This user doens't have authorization to complete this request.";
                            break;
                        case 404:
                            message = $"No data found to identification {identification}.";
                            break;
                        default:
                            message = "An error occurred while processing the request.";
                            break;
                    }

                    throw new Exception(message);
                }

                return JsonConvert.DeserializeObject<ClientResponse>(response.Content);
            }
            catch (Exception) { throw; }
        }

        public int Delete(string identification, UserResponse user)
        {
            try
            {
                var client = new RestClient(_endpoint + identification)
                {
                    Timeout = -1
                };

                var request = new RestRequest(Method.DELETE);

                request.AddHeader("Authorization", $"Bearer {user.Token.Token}");

                request.AddHeader("Content-Type", "application/json");

                request.AddParameter("application/json", ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                return (int)response.StatusCode;
            }
            catch (Exception) { throw; }
        }
    }
}
