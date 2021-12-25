namespace ERP.Store.API.Entities.Models.InputModel
{
    public class ClientInputModel
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Identification { get; set; }

        public AddressInputModel Address { get; set; }

        public ContactInputModel Contact { get; set; }

        public ImageInputModel Image { get; set; }
    }
}
