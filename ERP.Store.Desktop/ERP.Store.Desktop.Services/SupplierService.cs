using System;
using System.Configuration;
using ERP.Store.Desktop.Repositories;
using ERP.Store.Desktop.Entities.JSON.Request;

namespace ERP.Store.Desktop.Services
{
    public class SupplierService
    {
        private APIRepository _apiRepository { get; set; }

        public SupplierService()
        {
            _apiRepository = new APIRepository();
        }

        public dynamic Get(string identification, dynamic user)
        {
            try
            {
                _apiRepository.Endpoint = ConfigurationManager.ConnectionStrings["SupplierEndpoint"].ConnectionString + identification;

                var response = _apiRepository.Get(user);

                if (response == null) throw new Exception("It was not possible to complete the request.");

                return response;
            }
            catch (Exception) { throw; }
        }

        public int Post(SupplierRequest client, dynamic user)
        {
            try
            {
                var json = CreateJson(client);

                _apiRepository.Endpoint = ConfigurationManager.ConnectionStrings["SupplierEndpoint"].ConnectionString;

                var response = _apiRepository.Post(json, user);

                if (response == null) throw new Exception("It was not possible to complete the request.");

                return response.id;
            }
            catch (Exception) { throw; }
        }

        private static string CreateJson(SupplierRequest request)
        {
            try
            {
                #region CreateJson

                return
                @"{
                    " + "\n" +
                                    $@"  ""name"": ""{request.Name}"",
                    " + "\n" +
                                    $@"  ""identification"": ""{request.Identification}"",
                    " + "\n" +
                                    @"  ""address"": {
                    " + "\n" +
                                    $@"    ""zip"": ""{request.Address.Zip}"",
                    " + "\n" +
                                    $@"    ""street"": ""{request.Address.Street}"",
                    " + "\n" +
                                    $@"    ""number"": ""{request.Address.Number}"",
                    " + "\n" +
                                    $@"    ""comment"": ""{request.Address.Comment}"",
                    " + "\n" +
                                    $@"    ""neighborhood"": ""{request.Address.Neighborhood}"",
                    " + "\n" +
                                    $@"    ""city"": ""{request.Address.City}"",
                    " + "\n" +
                                    $@"    ""state"": ""{request.Address.State}"",
                    " + "\n" +
                                    $@"    ""country"": ""{request.Address.Country}""
                    " + "\n" +
                                    @"  },
                    " + "\n" +
                                    @"  ""contact"": {
                    " + "\n" +
                                    $@"    ""email"": ""{request.Contact.Email}"",
                    " + "\n" +
                                    $@"    ""cellphone"": ""{request.Contact.Cellphone}"",
                    " + "\n" +
                                    $@"    ""phone"": ""{request.Contact.Phone}""
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
