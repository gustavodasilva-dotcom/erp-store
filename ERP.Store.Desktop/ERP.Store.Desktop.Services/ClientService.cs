using System;
using System.Configuration;
using ERP.Store.Desktop.Repositories;
using ERP.Store.Desktop.Entities.JSON.Request;

namespace ERP.Store.Desktop.Services
{
    public class ClientService
    {
        private APIRepository _apiRepository { get; set; }

        public ClientService()
        {
            _apiRepository = new APIRepository();
        }

        public int Post(ClientRequest client, dynamic user)
        {
            try
            {
                var json = CreateJson(client);

                _apiRepository.Endpoint = ConfigurationManager.ConnectionStrings["ClientEndpoint"].ConnectionString;

                var response = _apiRepository.Post(json, user);

                if (response == null) throw new Exception("It was not possible to complete the request.");

                return response.id;
            }
            catch (Exception) { throw; }
        }

        public int Put(ClientRequest client, dynamic user)
        {
            try
            {
                var json = CreateJson(client);

                _apiRepository.Endpoint = ConfigurationManager.ConnectionStrings["ClientEndpoint"].ConnectionString;

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
                _apiRepository.Endpoint = ConfigurationManager.ConnectionStrings["ClientEndpoint"].ConnectionString + identification;

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
                _apiRepository.Endpoint = ConfigurationManager.ConnectionStrings["ClientEndpoint"].ConnectionString + identification;

                var response = _apiRepository.Delete(user);

                if (response == null) throw new Exception("It was not possible to complete the request.");

                return response;
            }
            catch (Exception) { throw; }
        }

        private string CreateJson(ClientRequest clientRequest)
        {
            try
            {
                #region CreateJson

                return
                @"{
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
            }
            catch (Exception) { throw; }
        }
    }
}
