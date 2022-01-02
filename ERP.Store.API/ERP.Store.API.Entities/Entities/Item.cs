namespace ERP.Store.API.Entities.Entities
{
    public class Item : EntityBase
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public Category Category { get; set; }

        public Inventory Inventory { get; set; }

        public Image Image { get; set; }
    }
}
