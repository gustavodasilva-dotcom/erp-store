using System.Collections.Generic;

namespace ERP.Store.API.Entities.Models.InputModel
{
    public class OrderInputModel
    {
        public string ClientIdentification { get; set; }

        public List<ItemOrderInputModel> Items { get; set; }

        public PaymentInputModel Payment { get; set; }
    }
}
