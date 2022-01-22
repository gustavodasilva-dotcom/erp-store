using System;

namespace ERP.Store.API.Entities.Tables
{
    public class OrdersTable
    {
        public int OrderID { get; set; }

        public int ClientID { get; set; }

        public int Deleted { get; set; }

        public DateTime InsertDate { get; set; }

        public int OrderCompleted { get; set; }
    }
}
