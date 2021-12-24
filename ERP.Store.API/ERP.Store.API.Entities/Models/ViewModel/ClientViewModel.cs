namespace ERP.Store.API.Entities.Models.ViewModel
{
    public class ClientViewModel
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Identification { get; set; }

        public AddressViewModel Address { get; set; }

        public ContactViewModel Contact { get; set; }

        public ImageViewModel Image { get; set; }
    }
}
