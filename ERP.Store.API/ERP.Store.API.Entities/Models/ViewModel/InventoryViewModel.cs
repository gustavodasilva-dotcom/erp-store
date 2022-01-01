using ERP.Store.API.Entities.Models.ViewModel.ItemViewModels;

namespace ERP.Store.API.Entities.Models.ViewModel
{
    public class InventoryViewModel
    {
        public int Quantity { get; set; }

        public SupplierDataViewModel Supplier { get; set; }
    }
}
