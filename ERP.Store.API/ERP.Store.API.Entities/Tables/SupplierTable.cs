using System;

namespace ERP.Store.API.Entities.Tables
{
    public class SupplierTable
    {
        public int SupplierID { get; set; }

        public string Name { get; set; }

        public string Identification { get; set; }

        public int ContactID { get; set; }

        public int AddressID { get; set; }

        public int Deleted { get; set; }

        public DateTime InsertDate { get; set; }
    }
}
