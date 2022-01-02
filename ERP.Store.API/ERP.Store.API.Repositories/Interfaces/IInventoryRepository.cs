using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface IInventoryRepository
    {
        Task<ItemData> GetItemAsync(int itemID);

        Task<Items_InventoryData> GetInventoryAsync(int itemID);

        Task<int> InsertItemAsync(Item item);

        Task InsertInventoryAsync(Item item);

        Task UpdateItemAsync(Item item);

        Task UpdateInventoryAsync(Item item);

        Task<CategoryData> GetCategoryAsync(int categoryID);
    }
}
