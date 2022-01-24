using ERP.Store.Desktop.Repositories;

namespace ERP.Store.Desktop.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService()
        {
            _orderRepository = new OrderRepository();
        }
    }
}
