using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ERP.Store.API.CustomExceptions;
using ERP.Store.API.Entities.Entities;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Repositories.Interfaces;
using ERP.Store.API.Entities.Models.InputModel;
using ERP.Store.API.Services.CustomExceptions;

namespace ERP.Store.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IClientService _clientService;

        private readonly IPaymentService _paymentService;

        private readonly IOrderRepository _orderRepository;

        private readonly IInventoryService _inventoryService;

        public OrderService(IClientService clientService, IPaymentService paymentService, IOrderRepository orderRepository, IInventoryService inventoryService)
        {
            _clientService = clientService;

            _paymentService = paymentService;

            _orderRepository = orderRepository;

            _inventoryService = inventoryService;
        }

        public async Task<dynamic> GetOrderAsync(int orderID)
        {
            try
            {
                var order = await _orderRepository.GetOrderAsync(orderID);

                if (order == null)
                    throw new NotFoundException($"There's no order registered with the id {orderID}.");

                var client = await _clientService.GetClientAsync(order.ClientID);
                client.Image = null;

                var orderPayment = await _paymentService.GetOrderPaymentAsync(orderID);

                dynamic paymentInfo = null;

                if (!orderPayment.PaymentID.Equals(1))
                    paymentInfo = await _paymentService.GetOrderPaymentInfoAsync(orderPayment);

                var orderItems = await _inventoryService.GetOrderItemsAsync(orderID);

                var items = new List<dynamic>();

                foreach (var orderItem in orderItems)
                {
                    var item = await _inventoryService.GetItemAsync(orderItem.ItemID);

                    if (item != null)
                    {
                        items.Add(new
                        {
                            orderItem.ItemID,
                            orderItem.Quantity,
                            item.Category,
                            item.Inventory
                        });
                    }
                }

                dynamic cardPayment = null;

                if (orderPayment.PaymentID.Equals(3) || orderPayment.PaymentID.Equals(4))
                {
                    cardPayment = new
                    {
                        paymentInfo.NameOnCard,
                        paymentInfo.CardNumber,
                        paymentInfo.YearExpiryDate,
                        paymentInfo.MonthExpiryDate
                    };
                }

                dynamic bankPayment = null;

                if (orderPayment.PaymentID.Equals(2) || orderPayment.PaymentID.Equals(5) || orderPayment.PaymentID.Equals(6))
                {
                    bankPayment = new
                    {
                        paymentInfo.Number,
                        paymentInfo.Agency,
                        paymentInfo.BankName
                    };
                }

                var payment = await _paymentService.GetPaymentInfoAsync(orderPayment.PaymentID);

                return new
                {
                    order.OrderID,
                    Payment = new
                    {
                        orderPayment.PaymentID,
                        payment.Description,
                        PaymentInfo = cardPayment == null ? bankPayment : cardPayment
                    },
                    Client = client,
                    Items = items,
                    IsCompleted = order.OrderCompleted == 0 ? false : true,
                    IsCanceled = order.Deleted == 0 ? false : true
                };
            }
            catch (Exception) { throw; }
        }

        public async Task CompleteOrCancelOrderAsync(CompleteOrderInputModel inputModel)
        {
            try
            {
                var order = await _orderRepository.GetOrderAsync(inputModel.OrderID);

                if (order == null || order.Deleted.Equals(1))
                    throw new NotFoundException($"There's no order registered with the id {inputModel.OrderID}.");

                if (inputModel.CompleteOrder && order.OrderCompleted.Equals(1))
                    throw new BadRequestException($"The order {inputModel.OrderID} is already completed.");

                if (inputModel.CancelOrder && order.Deleted.Equals(1))
                    throw new BadRequestException($"The order {inputModel.OrderID} is already canceled.");

                if (inputModel.CompleteOrder && inputModel.CancelOrder) throw new BadRequestException("The operation must to complete or delete an order.");

                if (inputModel.CompleteOrder) await _orderRepository.CompleteOrderAsync(inputModel.OrderID, true);

                if (inputModel.CancelOrder)
                {
                    await _orderRepository.CompleteOrderAsync(inputModel.OrderID, false);

                    var orderItems = await _inventoryService.GetOrderItemsAsync(inputModel.OrderID);

                    var items = new List<Item>();

                    foreach (var orderItem in orderItems)
                    {
                        var item = await _inventoryService.GetItemAsync(orderItem.ItemID);

                        if (item != null)
                        {
                            items.Add(new Item
                            {
                                ID = orderItem.ItemID,
                                Inventory = new Inventory
                                {
                                    Quantity = orderItem.Quantity
                                }
                            });
                        }
                    }

                    foreach (var item in items) await _inventoryService.UpdateInventoryAsync(item, false);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<int> RegisterOrderAsync(OrderInputModel input)
        {
            try
            {
                var itemsInput = new List<Item>();

                foreach (var item in input.Items)
                {
                    itemsInput.Add(new Item
                    {
                        ID = item.ItemID,
                        Inventory = new Inventory
                        {
                            Quantity = item.Quantity
                        }
                    });
                }

                var orderInput = new Order
                {
                    ClientIdentification = input.ClientIdentification,
                    Items = itemsInput,
                    Payment = new Payment
                    {
                        IsCheck = input.Payment.IsCheck,
                        IsCard = input.Payment.IsCard,
                        IsBankTransfer = input.Payment.IsBankTransfer
                    }
                };

                if (orderInput.Payment.IsCard)
                {
                    orderInput.Payment.Card = new Card
                    {
                        IsCredit = input.Payment.Card.IsCredit,
                        NameOnCard = input.Payment.Card.NameOnCard,
                        CardNumber = input.Payment.Card.CardNumber,
                        YearExpiryDate = input.Payment.Card.YearExpiryDate,
                        MonthExpiryDate = input.Payment.Card.MonthExpiryDate,
                        SecurityCode = input.Payment.Card.SecurityCode
                    };
                }

                if (orderInput.Payment.IsBankTransfer || orderInput.Payment.IsCheck)
                {
                    orderInput.Payment.BankInfo = new BankInfo
                    {
                        IsMobileTransfer = input.Payment.BankInfo.IsMobileTransfer,
                        IsEletronicBankTransfer = input.Payment.BankInfo.IsEletronicBankTransfer,
                        Number = input.Payment.BankInfo.Number,
                        Agency = input.Payment.BankInfo.Agency,
                        BankName = input.Payment.BankInfo.BankName
                    };
                }

                var client = await _clientService.GetClientAsync(orderInput.ClientIdentification);

                if (client == null)
                    throw new NotFoundException($"There's no user registered with the {orderInput.ClientIdentification} identification number.");

                orderInput.ClientID = client.ID;

                var messages = await _inventoryService.ValidateItemsAsync(itemsInput);

                if (messages.Any()) throw new NotFoundException(messages.FirstOrDefault());

                await _paymentService.ValidatePaymentMethodAsync(orderInput.Payment);

                orderInput.Value = await _paymentService.GetOrderValueAsync(itemsInput);

                orderInput.ID = await _orderRepository.InsertOrderAsync(orderInput);

                if (orderInput.ID == 0) throw new Exception("An error occurred while trying to insert the order header.");

                await _orderRepository.InsertOrderItemsAsync(orderInput);

                foreach (var item in itemsInput) await _inventoryService.UpdateInventoryAsync(item, true);

                orderInput.Payment.ID = await _paymentService.InsertOrderPaymentAsync(orderInput);

                if (orderInput.Payment.IsCard || orderInput.Payment.IsCheck || orderInput.Payment.IsBankTransfer)
                    await _paymentService.InsertPaymentInfoAsync(orderInput.Payment);

                return orderInput.ID;
            }
            catch (Exception) { throw; }
        }
    }
}
