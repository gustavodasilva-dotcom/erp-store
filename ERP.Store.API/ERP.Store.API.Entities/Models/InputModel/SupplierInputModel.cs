namespace ERP.Store.API.Entities.Models.InputModel
{
    public class SupplierInputModel
    {
        public string Name { get; set; }

        public string Identification { get; set; }

        public AddressInputModel Address { get; set; }

        public ContactInputModel Contact { get; set; }
    }
}
