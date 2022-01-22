using System;

namespace ERP.Store.API.Entities.Tables
{
    public class Order_PaymentTable
    {
        public int Order_PaymentID { get; set; }

        public double Value { get; set; }

        public int OrderID { get; set; }

        public int PaymentID { get; set; }

        public int PaymentStatusID { get; set; }

        public int Deleted { get; set; }

        public DateTime InsertDate { get; set; }
    }
}
