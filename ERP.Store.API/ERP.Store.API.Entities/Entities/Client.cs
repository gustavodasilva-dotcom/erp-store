namespace ERP.Store.API.Entities.Entities
{
    public class Client : EntityBase
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Identification { get; set; }

        public Address Address { get; set; }

        public Contact Contact { get; set; }

        public Image Image { get; set; }
    }
}
