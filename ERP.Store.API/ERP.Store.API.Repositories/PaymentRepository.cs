using Dapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;
using ERP.Store.API.Repositories.Interfaces;

namespace ERP.Store.API.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly string _connectionString;

        public PaymentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<int> InsertOrderPaymentAsync(Order order)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"EXEC uspRegisterOrderPayment @value, @orderID, @paymentID, @paymentStatusID;";

                    #endregion

                    return await db.ExecuteScalarAsync<int>(query, new
                    {
                        @value = order.Value,
                        @orderID = order.ID,
                        @paymentID = order.Payment.ID,
                        @paymentStatusID = order.Payment.RequiresConfirmation ? 2 : 1
                    },
                    commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task InsertPaymentInfoAsync(Payment payment)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = string.Empty;

                    if (payment.IsCard)
                    {
                        query = @"EXEC uspInsertCardsInfo @nameOnCard, @cardNumber, @yearExpiryDate, @monthExpiryDate, @securityCode, @orderPaymentID;";

                        await db.ExecuteAsync(query, new
                        {
                            @nameOnCard = payment.Card.NameOnCard,
                            @cardNumber = payment.Card.CardNumber,
                            @yearExpiryDate = payment.Card.YearExpiryDate,
                            @monthExpiryDate = payment.Card.MonthExpiryDate,
                            @securityCode = payment.Card.SecurityCode,
                            @orderPaymentID = payment.ID
                        },
                        commandTimeout: 30);
                    }
                    else
                    {
                        query = @"EXEC uspInsertBankInfo @number, @agency, @bankName, @orderPaymentID;";

                        await db.ExecuteAsync(query, new
                        {
                            @number = payment.BankInfo.Number,
                            @agency = payment.BankInfo.Agency,
                            @bankName = payment.BankInfo.BankName,
                            @orderPaymentID = payment.ID
                        },
                        commandTimeout: 30);
                    }

                    #endregion
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<PaymentsTable> GetPaymentInfoAsync(int paymentID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"SELECT * FROM Payments (NOLOCK) WHERE PaymentID = @paymentID;";

                    #endregion

                    return await db.QueryFirstOrDefaultAsync<PaymentsTable>(query, new { @paymentID = paymentID }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<CardsInfoTable> GetCardsInfoAsync(int orderPaymentID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"SELECT * FROM CardsInfo (NOLOCK) WHERE Order_PaymentID = @orderPaymentID AND Deleted = 0;";

                    #endregion

                    return await db.QueryFirstOrDefaultAsync<CardsInfoTable>(query, new { @orderPaymentID = orderPaymentID }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<BankInfoTable> GetBankInfoAsync(int orderPaymentID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"SELECT * FROM BankInfo (NOLOCK) WHERE Order_PaymentID = @orderPaymentID AND Deleted = 0;";

                    #endregion

                    return await db.QueryFirstOrDefaultAsync<BankInfoTable>(query, new { @orderPaymentID = orderPaymentID }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<Order_PaymentTable> GetOrderPaymentAsync(int orderID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"SELECT * FROM Order_Payment (NOLOCK) WHERE OrderID = @orderID AND Deleted = 0;";

                    #endregion

                    return await db.QueryFirstOrDefaultAsync<Order_PaymentTable>(query, new { @orderID = orderID }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task DeleteOrderPaymentAsync(int orderPaymentID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"UPDATE Order_Payment SET Deleted = 1 WHERE Order_PaymentID = @orderPaymentID;";

                    #endregion

                    await db.ExecuteAsync(query, new { @orderPaymentID = orderPaymentID },  commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }
    }
}
