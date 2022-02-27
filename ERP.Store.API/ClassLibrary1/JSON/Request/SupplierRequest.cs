namespace ERP.Store.Desktop.Entities.JSON.Request
{
    public class SupplierRequest
    {
        public string Name { get; set; }

        public string Identification { get; set; }

        public AddressRequest Address { get; set; }

        public ContactRequest Contact { get; set; }
    }
}
