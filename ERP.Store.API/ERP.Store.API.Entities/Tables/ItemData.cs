using System;

namespace ERP.Store.API.Entities.Tables
{
    public class ItemData
    {
        public int ItemID { get; set; }

        public int SupplierID { get; set; }

        public int Deleted { get; set; }

        public DateTime InsertDate { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int CategoryID { get; set; }
    }
}
