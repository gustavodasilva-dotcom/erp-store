using System.Collections.Generic;

namespace ERP.Store.Desktop.Entities.JSON.Request
{
    public class OrderRequest
    {
        public string ClientIdentification { get; set; }

        public List<ItemOrderRequest> Items { get; set; }

        public PaymentRequest Payment { get; set; }
    }
}
