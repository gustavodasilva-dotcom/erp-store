namespace ERP.Store.Desktop.Entities.JSON.Request
{
    public class ClientRequest
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Identification { get; set; }

        public AddressRequest Address { get; set; }

        public ContactRequest Contact { get; set; }

        public ImageRequest Image { get; set; }
    }
}
