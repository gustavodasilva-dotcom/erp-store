﻿using RestSharp;
using System;
using Newtonsoft.Json;
using ERP.Store.Desktop.Entities.Entities;

namespace ERP.Store.Desktop.Repositories
{
    public class APIRepository
    {
        public string Endpoint { get; set; }

        public dynamic Post(string json, dynamic user)
        {
            try
            {
                var client = new RestClient(Endpoint)
                {
                    Timeout = -1
                };

                var request = new RestRequest
                {
                    Method = Method.POST
                };

                request.AddHeader("Authorization", $"Bearer {user.token.token}");

                request.AddHeader("Content-Type", "application/json");

                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var response = client.Execute(request);

                if ((int)response.StatusCode != 201 || (int)response.StatusCode != 200)
                {
                    var responseContent = JsonConvert.DeserializeObject<dynamic>(response.Content);

                    throw new Exception(Convert.ToString(responseContent.messages).Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", ""));
                }

                return JsonConvert.DeserializeObject<dynamic>(response.Content);
            }
            catch (Exception) { throw; }
        }

        public dynamic Get(dynamic user)
        {
            try
            {
                var client = new RestClient(Endpoint)
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
                    var responseContent = JsonConvert.DeserializeObject<dynamic>(response.Content);

                    throw new Exception(Convert.ToString(responseContent.messages).Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", ""));
                }

                return JsonConvert.DeserializeObject<dynamic>(response.Content);
            }
            catch (Exception) { throw; }
        }

        // TODO: Implement later.
        //public dynamic Get(dynamic user)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception) { throw; }
        //}
    }
}
