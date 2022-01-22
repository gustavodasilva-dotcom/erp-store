using System;

namespace ERP.Store.API.Entities.Tables
{
    public class PaymentsTable
    {
        public int PaymentID { get; set; }

        public string Description { get; set; }

        public int RequiresConfirmation { get; set; }

        public int Deleted { get; set; }

        public DateTime InsertDate { get; set; }
    }
}
