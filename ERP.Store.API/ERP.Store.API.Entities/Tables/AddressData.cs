using System;

namespace ERP.Store.API.Entities.Tables
{
    public class AddressData
    {
        public int AddressID { get; set; }

        public string Zip { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Comment { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public int Deleted { get; set; }

        public DateTime InsertDate { get; set; }
    }
}
