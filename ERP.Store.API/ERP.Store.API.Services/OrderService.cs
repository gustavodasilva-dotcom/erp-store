using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.CustomExceptions;
using ERP.Store.API.Entities.Entities;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Repositories.Interfaces;
using ERP.Store.API.Services.CustomExceptions;
using ERP.Store.API.Entities.Models.InputModel;

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

                if (order == null) throw new NotFoundException($"There's no order registered with the id {orderID}.");

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
                            item.Price,
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
                        TotalValue = orderPayment.Value,
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

                if (inputModel.CompleteOrder) await _orderRepository.CompleteOrCancelOrderAsync(inputModel.OrderID, true);

                if (inputModel.CancelOrder)
                {
                    await _orderRepository.CompleteOrCancelOrderAsync(inputModel.OrderID, false);

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
                var orderInput = InitializeObjects(input);

                var client = await _clientService.GetClientAsync(orderInput.ClientIdentification);

                if (client == null)
                    throw new NotFoundException($"There's no user registered with the {orderInput.ClientIdentification} identification number.");

                orderInput.ClientID = client.ID;

                var messages = await _inventoryService.ValidateItemsAsync(orderInput.Items);

                if (messages.Any()) throw new NotFoundException(messages.FirstOrDefault());

                await _paymentService.ValidatePaymentMethodAsync(orderInput.Payment);

                orderInput.Value = await _paymentService.GetOrderValueAsync(orderInput.Items);

                orderInput.ID = await _orderRepository.InsertOrderAsync(orderInput);

                if (orderInput.ID == 0) throw new Exception("An error occurred while trying to insert the order header.");

                await _orderRepository.InsertOrderItemsAsync(orderInput);

                foreach (var item in orderInput.Items) await _inventoryService.UpdateInventoryAsync(item, true);

                orderInput.Payment.ID = await _paymentService.InsertOrderPaymentAsync(orderInput);

                if (orderInput.Payment.IsCard || orderInput.Payment.IsCheck || orderInput.Payment.IsBankTransfer)
                    await _paymentService.InsertPaymentInfoAsync(orderInput.Payment);

                return orderInput.ID;
            }
            catch (Exception) { throw; }
        }

        public async Task UpdateOrderAsync(OrderInputModel model, int orderID)
        {
            try
            {
                var orderInput = InitializeObjects(model);

                var order = await _orderRepository.GetOrderAsync(orderID);

                if (order == null) throw new NotFoundException($"There's no order registered with the id {orderID}.");

                orderInput.ID = orderID;

                var clientOfTheOrder = await _clientService.GetClientAsync(order.ClientID);

                var orderPayment = await _paymentService.GetOrderPaymentAsync(orderID);

                if (order.OrderCompleted.Equals(1))
                    throw new ConflictException($"It is not possible to update the order {orderID}, because it is completed.");

                if (order.Deleted.Equals(1))
                    throw new ConflictException($"It is not possible to update the order {orderID}, because it is canceled.");

                if (!clientOfTheOrder.Identification.Equals(model.ClientIdentification))
                    await UpdateOrderClientAsync(model, orderID);

                await UpdateItemsAsync(orderInput, orderID);

                await _paymentService.ValidatePaymentMethodAsync(orderInput.Payment);

                if (!orderInput.Payment.ID.Equals(orderPayment.PaymentID))
                    await UpdatePaymentAsync(model, orderPayment, orderInput);
            }
            catch (Exception) { throw; }
        }

        private static Order InitializeObjects(OrderInputModel input)
        {
            try
            {
                #region InitializeObjects

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

                return orderInput;

                #endregion
            }
            catch (Exception) { throw; }
        }

        private async Task UpdateOrderValueAsync(Order orderInput)
        {
            try
            {
                #region UpdateOrderValueAsync

                var itemsOrder = await _inventoryService.GetOrderItemsAsync(orderInput.ID);

                var itemsInput = new List<Item>();

                foreach (var item in itemsOrder)
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

                orderInput.Value = await _paymentService.GetOrderValueAsync(itemsInput);

                await _orderRepository.UpdateOrderValueAsync(orderInput);

                #endregion
            }
            catch (Exception) { throw; }
        }

        private async Task UpdateOrderClientAsync(OrderInputModel model, int orderID)
        {
            try
            {
                #region UpdateOrderClientAsync

                var clientToBeUpdated = await _clientService.GetClientAsync(model.ClientIdentification);

                if (clientToBeUpdated == null) throw new NotFoundException($"There's no client registered with the identification {model.ClientIdentification}.");

                await _orderRepository.UpdateClientsOrderAsync(clientToBeUpdated.ID, orderID);

                #endregion
            }
            catch (Exception) { throw; }
        }

        private async Task UpdateItemsAsync(Order orderInput, int orderID)
        {
            try
            {
                #region UpdateItemsAsync

                var itemsOrder = await _inventoryService.GetOrderItemsAsync(orderID);

                foreach (var itemUpdate in orderInput.Items)
                {
                    foreach (var item in itemsOrder)
                    {
                        if (itemUpdate.ID.Equals(item.ItemID))
                        {
                            if (!item.Quantity.Equals(itemUpdate.Inventory.Quantity))
                            {
                                await _orderRepository.UpdateOrderItemQuantityAsync(orderID, item.ItemID, itemUpdate.Inventory.Quantity);

                                var quantityToBeUpdatedInventory = itemUpdate.Inventory.Quantity - item.Quantity;

                                itemUpdate.Inventory.Quantity = quantityToBeUpdatedInventory;

                                if (itemUpdate.Inventory.Quantity >= item.Quantity)
                                    await _inventoryService.UpdateInventoryAsync(itemUpdate, true);
                                else
                                    await _inventoryService.UpdateInventoryAsync(itemUpdate, false);
                            }
                        }
                    }
                }

                if (orderInput.Items.Count > itemsOrder.ToList().Count)
                {
                    foreach (var itemUpdate in orderInput.Items)
                    {
                        foreach (var item in itemsOrder)
                        {
                            if (!itemUpdate.ID.Equals(item.ItemID))
                            {
                                var messages = await _inventoryService.ValidateItemsAsync(itemUpdate);

                                if (messages.Any()) throw new NotFoundException(messages.FirstOrDefault());

                                await _orderRepository.InsertOrderItemsAsync(itemUpdate, orderID);

                                await _inventoryService.UpdateInventoryAsync(itemUpdate, true);
                            }
                        }
                    }
                }

                await UpdateOrderValueAsync(orderInput);

                #endregion
            }
            catch (Exception) { throw; }
        }

        private async Task UpdatePaymentAsync(OrderInputModel model, Order_PaymentTable orderPayment, Order order)
        {
            try
            {
                #region UpdatePaymentAsync
                
                if (orderPayment.PaymentStatusID.Equals(1))
                    throw new ConflictException($"It is not possible to update the payment of the order {order.ID}, because the payment is alredy completed.");

                if (!model.Payment.IsCheck && !model.Payment.IsCard && !model.Payment.IsBankTransfer && !orderPayment.PaymentID.Equals(1))
                {
                    await _paymentService.DeleteOrderPaymentAsync(orderPayment.Order_PaymentID);

                    await _paymentService.InsertOrderPaymentAsync(order);
                }

                if (model.Payment.IsCheck && !orderPayment.PaymentID.Equals(2))
                {
                    await _paymentService.DeleteOrderPaymentAsync(orderPayment.Order_PaymentID);

                    order.Payment.ID = await _paymentService.InsertOrderPaymentAsync(order);

                    await _paymentService.InsertPaymentInfoAsync(order.Payment);
                }

                if (model.Payment.IsCard && model.Payment.Card.IsCredit && !orderPayment.PaymentID.Equals(4))
                {
                    await _paymentService.DeleteOrderPaymentAsync(orderPayment.Order_PaymentID);

                    order.Payment.ID = await _paymentService.InsertOrderPaymentAsync(order);

                    await _paymentService.InsertPaymentInfoAsync(order.Payment);
                }

                if (model.Payment.IsCard && !model.Payment.Card.IsCredit && !orderPayment.PaymentID.Equals(3))
                {
                    await _paymentService.DeleteOrderPaymentAsync(orderPayment.Order_PaymentID);

                    order.Payment.ID = await _paymentService.InsertOrderPaymentAsync(order);

                    await _paymentService.InsertPaymentInfoAsync(order.Payment);
                }

                if (model.Payment.IsBankTransfer && model.Payment.BankInfo.IsMobileTransfer && !orderPayment.PaymentID.Equals(5))
                {
                    await _paymentService.DeleteOrderPaymentAsync(orderPayment.Order_PaymentID);

                    order.Payment.ID = await _paymentService.InsertOrderPaymentAsync(order);

                    await _paymentService.InsertPaymentInfoAsync(order.Payment);
                }

                if (model.Payment.IsBankTransfer && model.Payment.BankInfo.IsEletronicBankTransfer && !orderPayment.PaymentID.Equals(6))
                {
                    await _paymentService.DeleteOrderPaymentAsync(orderPayment.Order_PaymentID);

                    order.Payment.ID = await _paymentService.InsertOrderPaymentAsync(order);

                    await _paymentService.InsertPaymentInfoAsync(order.Payment);
                }

                #endregion
            }
            catch (Exception) { throw; }
        }
    }
}
