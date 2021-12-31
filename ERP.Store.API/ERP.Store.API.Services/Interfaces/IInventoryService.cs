using System.Threading.Tasks;
using ERP.Store.API.Entities.Models.ViewModel.ItemViewModels;
using ERP.Store.API.Entities.Models.InputModel.ItemInputModels;

namespace ERP.Store.API.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<ItemDataViewModel> GetItemAsync(int itemID);

        Task<int> RegisterItemAsync(ItemDataInputModel input);
    }
}
