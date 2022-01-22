using System;

namespace ERP.Store.API.Entities.Tables
{
    public class CardsInfoTable
    {
        public int CardsInfoID { get; set; }

        public string NameOnCard { get; set; }

        public string CardNumber { get; set; }

        public int YearExpiryDate { get; set; }

        public int MonthExpiryDate { get; set; }

        public int SecurityCode { get; set; }

        public int Order_PaymentID { get; set; }

        public int Deleted { get; set; }

        public DateTime InsertDate { get; set; }
    }
}
