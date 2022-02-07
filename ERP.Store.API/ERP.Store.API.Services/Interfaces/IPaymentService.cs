using System.Threading.Tasks;
using System.Collections.Generic;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;

namespace ERP.Store.API.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<int> InsertOrderPaymentAsync(Order order);

        Task InsertPaymentInfoAsync(Payment payment);

        Task<double> GetOrderValueAsync(List<Item> items);

        Task<Order_PaymentTable> GetOrderPaymentAsync(int orderID);

        Task<PaymentsTable> GetPaymentInfoAsync(int paymentID);

        Task<dynamic> GetOrderPaymentInfoAsync(Order_PaymentTable orderPayment);

        Task DeleteOrderPaymentAsync(int orderPaymentID);

        Task<Payment> ValidatePaymentMethodAsync(Payment payment);
    }
}
