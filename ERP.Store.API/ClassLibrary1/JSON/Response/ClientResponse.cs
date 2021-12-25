namespace ERP.Store.Desktop.Entities.JSON.Response
{
    public class ClientResponse
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Identification { get; set; }

        public AddressResponse Address { get; set; }

        public ContactResponse Contact { get; set; }

        public ImageResponse Image { get; set; }
    }
}
