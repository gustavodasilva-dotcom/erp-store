namespace ERP.Store.Desktop.Entities.JSON.Request
{
    public class ItemRequest
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public CategoryRequest Category { get; set; }

        public SupplierRequest Supplier { get; set; }

        public ImageRequest Image { get; set; }
    }
}
