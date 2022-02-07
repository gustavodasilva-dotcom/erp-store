using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task<int> InsertOrderPaymentAsync(Order order);

        Task InsertPaymentInfoAsync(Payment payment);

        Task<PaymentsTable> GetPaymentInfoAsync(int paymentID);

        Task<CardsInfoTable> GetCardsInfoAsync(int orderPaymentID);

        Task<BankInfoTable> GetBankInfoAsync(int orderPaymentID);

        Task<Order_PaymentTable> GetOrderPaymentAsync(int orderID);

        Task DeleteOrderPaymentAsync(int orderPaymentID);
    }
}
