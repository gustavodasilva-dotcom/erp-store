using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> InsertOrderAsync(Order order);

        Task InsertOrderItemsAsync(Order order);

        Task<int> InsertOrderPaymentAsync(Order order);

        Task InsertPaymentInfoAsync(Payment payment);

        Task<PaymentsTable> GetPaymentInfoAsync(int paymentID);
    }
}
