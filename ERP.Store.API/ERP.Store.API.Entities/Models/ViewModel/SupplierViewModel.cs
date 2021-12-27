namespace ERP.Store.API.Entities.Models.ViewModel
{
    public class SupplierViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Identification { get; set; }

        public AddressViewModel Address { get; set; }

        public ContactViewModel Contact { get; set; }
    }
}
