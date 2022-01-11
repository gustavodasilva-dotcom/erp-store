using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrdersTable> GetOrderAsync(int orderID);

        Task<int> InsertOrderAsync(Order order);

        Task InsertOrderItemsAsync(Order order);
    }
}
