using System;
using System.Configuration;
using ERP.Store.Desktop.Repositories;
using ERP.Store.Desktop.Entities.JSON.Request;

namespace ERP.Store.Desktop.Services
{
    public class OrderService
    {
        private APIRepository _apiRepository { get; set; }

        public OrderService()
        {
            _apiRepository = new APIRepository();
        }

        public dynamic Get(int orderID, dynamic user)
        {
            try
            {
                _apiRepository.Endpoint = ConfigurationManager.ConnectionStrings["OrderEndpoint"].ConnectionString + orderID;

                var response = _apiRepository.Get(user);

                if (response == null) throw new Exception("It was not possible to complete the request.");

                return response;
            }
            catch (Exception) { throw; }
        }

        public int Post(OrderRequest order, dynamic user)
        {
            try
            {
                var json = CreateJson(order);

                _apiRepository.Endpoint = ConfigurationManager.ConnectionStrings["OrderEndpoint"].ConnectionString;

                var response = _apiRepository.Post(json, user);

                if (response == null) throw new Exception("It was not possible to complete the request.");

                return response.orderID;
            }
            catch (Exception) { throw; }
        }

        public dynamic Put(OrderRequest order, int orderID, dynamic user)
        {
            try
            {
                var json = CreateJson(order);

                _apiRepository.Endpoint = ConfigurationManager.ConnectionStrings["OrderEndpoint"].ConnectionString + orderID;

                var response = _apiRepository.Put(json, user);

                if (response == null) throw new Exception("It was not possible to complete the request.");

                return response.orderID;
            }
            catch (Exception) { throw; }
        }

        private string CreateJson(OrderRequest order)
        {
            try
            {
                var jsonItems = CreateItemsJson(order);

                #region CreateJson

                return @"{
                " + "\n" +
                                $@"  ""clientIdentification"": ""{order.ClientIdentification}"",
                " + "\n" +
                                $@"  ""items"": [{jsonItems}],
                " + "\n" +
                                @"  ""payment"": {
                " + "\n" +
                                $@"    ""isCheck"": {Convert.ToString(order.Payment.IsCheck).ToLower()},
                " + "\n" +
                                $@"    ""isCard"": {Convert.ToString(order.Payment.IsCard).ToLower()},
                " + "\n" +
                                $@"    ""isBankTransfer"": {Convert.ToString(order.Payment.IsBankTransfer).ToLower()},
                " + "\n" +
                                @"    ""card"": {
                " + "\n" +
                                $@"      ""isCredit"": {Convert.ToString(order.Payment.Card.IsCredit).ToLower()},
                " + "\n" +
                                $@"      ""nameOnCard"": ""{order.Payment.Card.NameOnCard}"",
                " + "\n" +
                                $@"      ""cardNumber"": ""{order.Payment.Card.CardNumber}"",
                " + "\n" +
                                $@"      ""yearExpiryDate"": {order.Payment.Card.YearExpiryDate},
                " + "\n" +
                                $@"      ""monthExpiryDate"": {order.Payment.Card.MonthExpiryDate},
                " + "\n" +
                                $@"      ""securityCode"": {order.Payment.Card.SecurityCode}
                " + "\n" +
                                @"    },
                " + "\n" +
                                @"    ""bankInfo"": {
                " + "\n" +
                                $@"      ""isMobileTransfer"": {Convert.ToString(order.Payment.BankInfo.IsMobileTransfer).ToLower()},
                " + "\n" +
                                $@"      ""isEletronicBankTransfer"": {Convert.ToString(order.Payment.BankInfo.IsEletronicBankTransfer).ToLower()},
                " + "\n" +
                                $@"      ""number"": ""{order.Payment.BankInfo.Number}"",
                " + "\n" +
                                $@"      ""agency"": ""{order.Payment.BankInfo.Agency}"",
                " + "\n" +
                                $@"      ""bankName"": ""{order.Payment.BankInfo.BankName}""
                " + "\n" +
                                @"    }
                " + "\n" +
                                @"  }
                " + "\n" +
                @"}";

                #endregion
            }
            catch (Exception) { throw; }
        }

        private string CreateItemsJson(OrderRequest order)
        {
            try
            {
                #region CreateItemsJson

                string jsonItems = string.Empty, json = string.Empty;

                foreach (var item in order.Items)
                {
                    jsonItems = jsonItems + @"{
                    " + "\n" +
                                    $@"    ""itemID"": {item.ItemID},
                    " + "\n" +
                                    $@"    ""quantity"": {item.Quantity}
                    " + "\n" +
                    @"},";
                }

                #endregion

                return jsonItems.Remove(jsonItems.Length - 1);
            }
            catch (Exception) { throw; }
        }
    }
}
