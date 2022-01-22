namespace ERP.Store.API.Entities.Models.InputModel
{
    public class ItemInputModel
    {
        public int ItemID { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public CategoryInputModel Category { get; set; }

        public InventoryInputModel Inventory { get; set; }

        public ImageInputModel Image { get; set; }
    }
}
