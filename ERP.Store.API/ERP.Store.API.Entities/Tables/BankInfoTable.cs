using System;

namespace ERP.Store.API.Entities.Tables
{
    public class BankInfoTable
    {
        public int BankInfoID { get; set; }

        public string Number { get; set; }

        public string Agency { get; set; }

        public string BankName { get; set; }

        public int Order_PaymentID { get; set; }

        public int Deleted { get; set; }

        public DateTime InsertDate { get; set; }
    }
}
