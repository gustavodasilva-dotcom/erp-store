namespace ERP.Store.API.Entities.Entities
{
    public class Supplier : EntityBase
    {
        public string Name { get; set; }

        public string Identification { get; set; }

        public Address Address { get; set; }

        public Contact Contact { get; set; }
    }
}
