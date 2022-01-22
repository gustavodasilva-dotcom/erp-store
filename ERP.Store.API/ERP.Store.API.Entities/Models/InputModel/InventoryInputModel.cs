namespace ERP.Store.API.Entities.Models.InputModel
{
    public class InventoryInputModel
    {
        public int Quantity { get; set; }

        public SupplierInputModel Supplier { get; set; }
    }
}
