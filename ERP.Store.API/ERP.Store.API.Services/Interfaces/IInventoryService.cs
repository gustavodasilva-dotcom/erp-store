using System.Threading.Tasks;
using System.Collections.Generic;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;
using ERP.Store.API.Entities.Models.ViewModel;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<dynamic>> GetCategories();

        Task<ItemViewModel> GetItemAsync(int itemID);

        Task<int> RegisterItemAsync(ItemInputModel input);

        Task<int> UpdateItemInventoryAsync(ItemInputModel input);

        Task<IEnumerable<string>> ValidateItemsAsync(List<Item> items);

        Task<IEnumerable<Order_ItemTable>> GetOrderItemsAsync(int orderID);

        Task<IEnumerable<dynamic>> GetShortListOfItemsAsync();
    }
}
