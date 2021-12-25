using System;

namespace ERP.Store.API.Entities.Tables
{
    public class ClientData
    {
        public int ClientID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Identification { get; set; }

        public int ContactID { get; set; }

        public int AddressID { get; set; }

        public int Deleted { get; set; }

        public DateTime InsertDate { get; set; }
    }
}
