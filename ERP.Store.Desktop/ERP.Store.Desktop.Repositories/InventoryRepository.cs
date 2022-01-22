using RestSharp;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Collections.Generic;
using ERP.Store.Desktop.Entities.JSON.Request;

namespace ERP.Store.Desktop.Repositories
{
    public class InventoryRepository
    {
        private readonly string _endpoint;

        public InventoryRepository()
        {
            _endpoint = ConfigurationManager.ConnectionStrings["InventoryEndpoint"].ConnectionString;
        }

        public List<dynamic> Get(dynamic user)
        {
            try
            {
                var client = new RestClient(_endpoint)
                {
                    Timeout = -1
                };

                var request = new RestRequest(Method.GET);

                request.AddHeader("Authorization", $"Bearer {user.token.token}");

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
                            message = $"No categories were found.";
                            break;
                        default:
                            message = "An error occurred while processing the request.";
                            break;
                    }

                    throw new Exception(message);
                }

                return JsonConvert.DeserializeObject<List<dynamic>>(response.Content);
            }
            catch (Exception) { throw; }
        }

        public dynamic Get(string id, dynamic user)
        {
            try
            {
                var client = new RestClient(_endpoint + id)
                {
                    Timeout = -1
                };

                var request = new RestRequest(Method.GET);

                request.AddHeader("Authorization", $"Bearer {user.token.token}");

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
                            message = $"No data found to id {id}.";
                            break;
                        default:
                            message = "An error occurred while processing the request.";
                            break;
                    }

                    throw new Exception(message);
                }

                return JsonConvert.DeserializeObject<dynamic>(response.Content);
            }
            catch (Exception) { throw; }
        }

        public dynamic Post(ItemRequest item, dynamic user)
        {
            try
            {
                var client = new RestClient(_endpoint)
                {
                    Timeout = -1
                };
                
                var request = new RestRequest(Method.POST);
                
                request.AddHeader("Authorization", $"Bearer {user.token.token}");
                
                request.AddHeader("Content-Type", "application/json");

                #region JSONBody

                var body = @"{
                    " + "\n" +
                                    @"  ""itemID"": 0,
                    " + "\n" +
                                    $@"  ""name"": ""{item.Name}"",
                    " + "\n" +
                                    $@"  ""price"": {item.Price},
                    " + "\n" +
                                    @"  ""category"": {
                    " + "\n" +
                                    $@"    ""id"": {item.Category.CategoryID},
                    " + "\n" +
                                    @"    ""description"": """"
                    " + "\n" +
                                    @"  },
                    " + "\n" +
                                    @"  ""inventory"": {
                    " + "\n" +
                                    $@"    ""quantity"": {item.Quantity},
                    " + "\n" +
                                    @"    ""supplier"": {
                    " + "\n" +
                                    $@"      ""name"": ""{item.Supplier.Name}"",
                    " + "\n" +
                                    $@"      ""identification"": ""{item.Supplier.Identification}"",
                    " + "\n" +
                                    @"      ""address"": {
                    " + "\n" +
                                    @"        ""zip"": """",
                    " + "\n" +
                                    @"        ""street"": """",
                    " + "\n" +
                                    @"        ""number"": """",
                    " + "\n" +
                                    @"        ""comment"": """",
                    " + "\n" +
                                    @"        ""neighborhood"": """",
                    " + "\n" +
                                    @"        ""city"": """",
                    " + "\n" +
                                    @"        ""state"": """",
                    " + "\n" +
                                    @"        ""country"": """"
                    " + "\n" +
                                    @"      },
                    " + "\n" +
                                    @"      ""contact"": {
                    " + "\n" +
                                    @"        ""email"": """",
                    " + "\n" +
                                    @"        ""cellphone"": """",
                    " + "\n" +
                                    @"        ""phone"": """"
                    " + "\n" +
                                    @"      }
                    " + "\n" +
                                    @"    }
                    " + "\n" +
                                    @"  },
                    " + "\n" +
                                    @"  ""image"": {
                    " + "\n" +
                                    $@"    ""isImage"": {Convert.ToString(item.Image.IsImage).ToLower()},
                    " + "\n" +
                                    $@"    ""base64"": ""{item.Image.Base64}""
                    " + "\n" +
                                    @"  }
                    " + "\n" +
                @"}";

                #endregion

                request.AddParameter("application/json", body, ParameterType.RequestBody);

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
                            message = $"No data found to identification {item.Supplier.Identification}.";
                            break;
                        default:
                            message = "An error occurred while processing the request.";
                            break;
                    }

                    throw new Exception(message);
                }

                return JsonConvert.DeserializeObject<dynamic>(response.Content);
            }
            catch (Exception) { throw; }
        }
    }
}
