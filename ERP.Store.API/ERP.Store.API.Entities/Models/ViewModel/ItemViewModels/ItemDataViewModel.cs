namespace ERP.Store.API.Entities.Models.ViewModel.ItemViewModels
{
    public class ItemDataViewModel
    {
        public int ItemID { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public CategoryDataViewModel Category { get; set; }

        public InventoryViewModel Inventory { get; set; }

        public ImageViewModel Image { get; set; }
    }
}
