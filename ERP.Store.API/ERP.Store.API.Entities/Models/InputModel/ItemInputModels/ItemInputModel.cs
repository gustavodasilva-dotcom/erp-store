namespace ERP.Store.API.Entities.Models.InputModel.ItemInputModels
{
    public class ItemInputModel
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public CategoryInputModel Category { get; set; }

        public SupplierInputModel Supplier  { get; set; }

        public ImageInputModel Image { get; set; }
    }
}
