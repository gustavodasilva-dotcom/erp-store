using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Repositories.Interfaces;

namespace ERP.Store.API.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        
        private readonly IInventoryRepository _inventoryRepository;

        public PaymentService(IPaymentRepository paymentRepository, IInventoryRepository inventoryRepository)
        {
            _paymentRepository = paymentRepository;

            _inventoryRepository = inventoryRepository;
        }

        public async Task<int> InsertOrderPaymentAsync(Order order)
        {
            try
            {
                return await _paymentRepository.InsertOrderPaymentAsync(order);
            }
            catch (Exception) { throw; }
        }

        public async Task InsertPaymentInfoAsync(Payment payment)
        {
            try
            {
                await _paymentRepository.InsertPaymentInfoAsync(payment);
            }
            catch (Exception) { throw; }
        }

        public async Task<Order_PaymentTable> GetOrderPaymentAsync(int orderID)
        {
            try
            {
                return await _paymentRepository.GetOrderPaymentAsync(orderID);
            }
            catch (Exception) { throw; }
        }

        public async Task<dynamic> GetOrderPaymentInfoAsync(Order_PaymentTable orderPayment)
        {
            try
            {
                if (orderPayment.PaymentID.Equals(3) || orderPayment.PaymentID.Equals(4))
                    return await _paymentRepository.GetCardsInfoAsync(orderPayment.Order_PaymentID);
                else
                    return await _paymentRepository.GetBankInfoAsync(orderPayment.Order_PaymentID);
            }
            catch (Exception) { throw; }
        }

        public async Task<PaymentsTable> GetPaymentInfoAsync(int paymentID)
        {
            try
            {
                return await _paymentRepository.GetPaymentInfoAsync(paymentID);
            }
            catch (Exception) { throw; }
        }

        public async Task<double> GetOrderValueAsync(List<Item> items)
        {
            try
            {
                #region GetOrderValue

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

        public async Task<Payment> ValidatePaymentMethodAsync(Payment payment)
        {
            try
            {
                #region ValidatePayment

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

                var paymentInfo = await _paymentRepository.GetPaymentInfoAsync(payment.ID);

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
