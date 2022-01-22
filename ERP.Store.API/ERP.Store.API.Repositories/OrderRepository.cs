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

        public async Task<OrdersTable> GetOrderAsync(int orderID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"SELECT * FROM Orders (NOLOCK) WHERE OrderID = @orderID;";

                    #endregion

                    return await db.QueryFirstOrDefaultAsync<OrdersTable>(query, new { @orderID = orderID }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
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
    }
}
