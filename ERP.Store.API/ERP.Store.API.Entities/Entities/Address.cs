namespace ERP.Store.API.Entities.Entities
{
    public class Address : EntityBase
    {
        public string Zip { get; set; }

        public string Street { get; set; }

        public string Comment { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
    }
}
