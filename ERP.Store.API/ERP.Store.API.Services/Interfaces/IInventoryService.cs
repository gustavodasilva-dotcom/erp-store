using System.Threading.Tasks;
using ERP.Store.API.Entities.Models.ViewModel;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<ItemViewModel> GetItemAsync(int itemID);

        Task<int> RegisterItemAsync(ItemInputModel input);

        Task<int> UpdateItemInventoryAsync(ItemInputModel input);
    }
}
