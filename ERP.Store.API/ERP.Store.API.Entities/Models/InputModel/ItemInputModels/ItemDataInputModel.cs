namespace ERP.Store.API.Entities.Models.InputModel.ItemInputModels
{
    public class ItemDataInputModel
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public CategoryDataInputModel Category { get; set; }

        public SupplierDataInputModel Supplier  { get; set; }

        public ImageInputModel Image { get; set; }
    }
}
