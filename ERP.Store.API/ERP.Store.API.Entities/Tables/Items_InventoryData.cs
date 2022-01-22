using System;

namespace ERP.Store.API.Entities.Tables
{
    public class Items_InventoryData
    {
        public int Inventory_ItemID { get; set; }

        public int Quantity { get; set; }

        public int ItemID { get; set; }

        public int SupplierID { get; set; }

        public int Deleted { get; set; }

        public DateTime InsertDate { get; set; }
    }
}
