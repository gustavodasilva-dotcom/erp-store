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
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<int> InsertOrderAsync(Order order)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query =
                    @"  BEGIN TRANSACTION;

                        	BEGIN TRY
                        
                        		INSERT INTO Orders VALUES (@clientId, 0, GETDATE(), 0);
                        
                        		DECLARE @OrderID INT = @@IDENTITY;
                        
                        	END TRY
                        
                        	BEGIN CATCH
                        
                        		IF @@TRANCOUNT > 0
                        			ROLLBACK TRANSACTION;
                        
                        	END CATCH;
                        
                        IF @@TRANCOUNT > 0
                            COMMIT TRANSACTION;

                        SELECT @OrderID;";

                    #endregion

                    return await db.ExecuteScalarAsync<int>(query, new { @clientId = order.ClientID }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task InsertOrderItemsAsync(Order order)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    foreach (var item in order.Items)
                    {
                        #region SQL

                        var query = @"EXEC uspInsertOrderItem @orderID, @itemID, @quantity;";

                        #endregion

                        await db.ExecuteAsync(query, new
                        {
                            @orderID = order.ID,
                            @itemID = item.ID,
                            @quantity = item.Inventory.Quantity
                        },
                        commandTimeout: 30);
                    };
                }
            }
            catch (Exception) { throw; }
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
    }
}
