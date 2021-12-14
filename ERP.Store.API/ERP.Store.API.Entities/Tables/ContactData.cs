using System;

namespace ERP.Store.API.Entities.Tables
{
    public class ContactData
    {
        public int ContactID { get; set; }

        public string Email { get; set; }

        public string Cellphone { get; set; }

        public string Phone { get; set; }

        public int Deleted { get; set; }

        public DateTime InsertDate { get; set; }
    }
}
