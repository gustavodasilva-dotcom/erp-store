namespace ERP.Store.API.Entities.Models.InputModel
{
    public class AddressInputModel
    {
        public string Zip { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Comment { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
    }
}
