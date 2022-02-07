using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrdersTable> GetOrderAsync(int orderID);

        Task CompleteOrCancelOrderAsync(int orderID, bool completeOrder);

        Task<int> InsertOrderAsync(Order order);

        Task InsertOrderItemsAsync(Order order);

        Task InsertOrderItemsAsync(Item item, int orderID);

        Task UpdateClientsOrderAsync(int clientID, int orderID);

        Task UpdateOrderItemQuantityAsync(int orderID, int itemID, int quantity);

        Task UpdateOrderValueAsync(Order order);
    }
}
