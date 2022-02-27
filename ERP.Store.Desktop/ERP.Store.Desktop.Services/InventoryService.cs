using System;
using System.Configuration;
using ERP.Store.Desktop.Repositories;
using ERP.Store.Desktop.Entities.Entities;
using ERP.Store.Desktop.Entities.JSON.Request;

namespace ERP.Store.Desktop.Services
{
    public class InventoryService
    {
        private APIRepository _apiRepository { get; set; }

        public InventoryService()
        {
            _apiRepository = new APIRepository();
        }

        public dynamic Get(dynamic user, CategoryType categoryType)
        {
            try
            {
                var category = string.Empty;

                if (categoryType == CategoryType.Categories)
                    category = "categories";
                else
                    category = "items";

                _apiRepository.Endpoint = ConfigurationManager.ConnectionStrings["InventoryEndpoint"].ConnectionString + category;

                var response = _apiRepository.Get(user);

                if (response == null) throw new Exception("It was not possible to complete the request.");

                return response;
            }
            catch (Exception) { throw; }
        }

        public dynamic Get(string identification, dynamic user)
        {
            try
            {
                _apiRepository.Endpoint = ConfigurationManager.ConnectionStrings["InventoryEndpoint"].ConnectionString + identification;

                var response = _apiRepository.Get(user);

                if (response == null) throw new Exception("It was not possible to complete the request.");

                return response;
            }
            catch (Exception) { throw; }
        }

        public int Post(ItemRequest item, dynamic user)
        {
            try
            {
                var json = CreateJson(item);

                _apiRepository.Endpoint = ConfigurationManager.ConnectionStrings["InventoryEndpoint"].ConnectionString;

                var response = _apiRepository.Post(json, user);

                if (response == null) throw new Exception("It was not possible to complete the request.");

                return response.itemID;
            }
            catch (Exception) { throw; }
        }

        public int Put(ItemRequest item, dynamic user)
        {
            try
            {
                var json = CreateJson(item);

                _apiRepository.Endpoint = ConfigurationManager.ConnectionStrings["InventoryEndpoint"].ConnectionString;

                var response = _apiRepository.Put(json, user);

                if (response == null) throw new Exception("It was not possible to complete the request.");

                return response.itemID;
            }
            catch (Exception) { throw; }
        }

        private string CreateJson(ItemRequest item)
        {
            try
            {
                #region CreateJson

                return
                @"{
                    " + "\n" +
                                    $@"  ""itemID"": {item.ItemID},
                    " + "\n" +
                                    $@"  ""name"": ""{item.Name}"",
                    " + "\n" +
                                    $@"  ""price"": {Convert.ToString(item.Price).Replace(",", ".")},
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
            }
            catch (Exception) { throw; }
        }
    }
}
