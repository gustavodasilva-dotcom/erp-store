using System;

namespace ERP.Store.API.Entities.Tables
{
    public class Order_ItemTable
    {
        public int Order_ItemID { get; set; }

        public int OrderID { get; set; }

        public int ItemID { get; set; }

        public int Quantity { get; set; }

        public int Deleted { get; set; }

        public DateTime InsertDate { get; set; }
    }
}
