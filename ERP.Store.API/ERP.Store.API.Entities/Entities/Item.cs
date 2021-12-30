namespace ERP.Store.API.Entities.Entities
{
    public class Item : EntityBase
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public Category Category { get; set; }

        public Supplier Supplier { get; set; }

        public Image Image { get; set; }
    }
}
