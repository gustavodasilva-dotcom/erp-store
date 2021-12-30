namespace ERP.Store.API.Entities.Models.ViewModel.ItemViewModels
{
    public class ItemViewModel
    {
        public int ItemID { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public SupplierViewModel Supplier { get; set; }

        public CategoryViewModel Category { get; set; }
        
        public ImageViewModel Image { get; set; }
    }
}
