using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ERP.Store.API.CustomExceptions;
using ERP.Store.API.Entities.Entities;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Repositories.Interfaces;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IClientRepository _clientRepository;

        private readonly IInventoryRepository _inventoryRepository;

        public OrderService(IOrderRepository orderRepository, IClientRepository clientRepository, IInventoryRepository inventoryRepository)
        {
            _orderRepository = orderRepository;

            _clientRepository = clientRepository;

            _inventoryRepository = inventoryRepository;
        }

        public async Task RegisterOrderAsync(OrderInputModel input)
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

                if (orderInput.Payment.IsBankTransfer)
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

                var client = await _clientRepository.GetClientAsync(orderInput.ClientIdentification);

                if (client == null)
                    throw new NotFoundException($"There's no user registered with the {orderInput.ClientIdentification} identification number.");

                orderInput.ClientID = client.ClientID;

                var messages = await ValidateItemsAsync(itemsInput);

                if (messages.Any())
                    throw new NotFoundException(messages.FirstOrDefault());

                await ValidatePaymentMethodAsync(orderInput.Payment);

                orderInput.Value = await GetOrderValueAsync(itemsInput);

                orderInput.ID = await _orderRepository.InsertOrderAsync(orderInput);

                if (orderInput.ID == 0)
                    throw new Exception("An error occurred while trying to insert the order header.");

                await _orderRepository.InsertOrderItemsAsync(orderInput);

                orderInput.Payment.ID = await _orderRepository.InsertOrderPaymentAsync(orderInput);

                if (orderInput.Payment.IsCard || orderInput.Payment.IsCheck || orderInput.Payment.IsBankTransfer)
                    await _orderRepository.InsertPaymentInfoAsync(orderInput.Payment);
            }
            catch (Exception) { throw; }
        }

        private async Task<IEnumerable<string>> ValidateItemsAsync(List<Item> items)
        {
            try
            {
                #region ValidateItemsAsync

                var messages = new List<string>();

                foreach (var item in items)
                {
                    if (await _inventoryRepository.GetItemAsync(item.ID) == null)
                        messages.Add($"The id {item.ID} does not correspond to an actual item.");
                }

                #endregion

                return messages;
            }
            catch (Exception) { throw; }
        }

        private async Task<double> GetOrderValueAsync(List<Item> items)
        {
            try
            {
                #region GetOrderValueAsync

                double value = 0;

                foreach (var item in items)
                {
                    var getOrder = await _inventoryRepository.GetItemAsync(item.ID);

                    if (getOrder != null)
                        value += getOrder.Price * item.Inventory.Quantity;
                }

                #endregion

                return value;
            }
            catch (Exception) { throw; }
        }

        private async Task<Payment> ValidatePaymentMethodAsync(Payment payment)
        {
            try
            {
                #region ValidatePaymentMethod

                // Default (invalid payment method)
                payment.ID = 0;

                // Cash
                if (!payment.IsCheck && !payment.IsCard && !payment.IsBankTransfer)
                    payment.ID = 1;

                // Check
                if (payment.IsCheck)
                    payment.ID = 2;

                if (payment.IsCard)
                {
                    if (payment.Card.IsCredit)
                        // Credit card
                        payment.ID = 4;
                    else
                        // Debit card
                        payment.ID = 3;
                }

                if (payment.IsBankTransfer)
                {
                    if (payment.BankInfo.IsMobileTransfer)
                        // Mobile payments
                        payment.ID = 5;
                    else
                        // Electronic bank transfers
                        payment.ID = 6;
                }

                var paymentInfo = await _orderRepository.GetPaymentInfoAsync(payment.ID);

                if (paymentInfo == null)
                    throw new Exception("The payment method informed is invalid.");

                payment.RequiresConfirmation = paymentInfo.RequiresConfirmation == 1 ? true : false;

                #endregion

                return payment;
            }
            catch (Exception) { throw; }
        }
    }
}
