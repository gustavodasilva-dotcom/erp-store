using System.Threading.Tasks;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<dynamic> GetOrderAsync(int orderID);

        Task CompleteOrCancelOrderAsync(CompleteOrderInputModel inputModel);

        Task<int> RegisterOrderAsync(OrderInputModel input);
    }
}
