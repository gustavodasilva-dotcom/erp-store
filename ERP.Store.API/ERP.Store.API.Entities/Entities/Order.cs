using System.Collections.Generic;

namespace ERP.Store.API.Entities.Entities
{
    public class Order : EntityBase
    {
        public int ClientID { get; set; }

        public string ClientIdentification { get; set; }

        public double Value { get; set; }

        public List<Item> Items { get; set; }

        public Payment Payment { get; set; }
    }
}
