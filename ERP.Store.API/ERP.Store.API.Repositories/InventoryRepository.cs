using Dapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;
using ERP.Store.API.Repositories.Interfaces;

namespace ERP.Store.API.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly string _connectionString;

        public InventoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<ItemData> GetItemAsync(int itemID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"SELECT * FROM Items (NOLOCK) WHERE ItemID = @itemID AND Deleted = 0;";

                    #endregion

                    return await db.QueryFirstOrDefaultAsync<ItemData>(query, new { @itemID = itemID }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<Items_InventoryData> GetInventoryAsync(int itemID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"SELECT * FROM Items_Inventory (NOLOCK) WHERE ItemID = @itemID AND Deleted = 0;";

                    #endregion

                    return await db.QueryFirstOrDefaultAsync<Items_InventoryData>(query, new { @itemID = itemID }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<Order_ItemTable>> GetOrderItemsAsync(int orderID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"SELECT * FROM Order_Item (NOLOCK) WHERE OrderID = @orderID;";

                    #endregion

                    return await db.QueryAsync<Order_ItemTable>(query, new { @orderID = orderID }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<int> InsertItemAsync(Item item)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"EXEC uspRegisterItem @Name, @Price, @CategoryID, @SupplierID, @ImageID;";

                    #endregion

                    return await db.ExecuteScalarAsync<int>(query, new
                    {
                        @Name = item.Name,
                        @Price = item.Price,
                        @CategoryID = item.Category.ID,
                        @SupplierID = item.Inventory.Supplier.ID,
                        @ImageID = item.Image.ID
                    },
                    commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task InsertInventoryAsync(Item item)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"EXEC uspRegisterInventory @itemID, @supplierID, @quantity;";

                    #endregion

                    await db.ExecuteAsync(query, new
                    {
                        @itemID = item.ID,
                        @supplierID = item.Inventory.Supplier.ID,
                        @quantity = item.Inventory.Quantity
                    },
                    commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task UpdateItemAsync(Item item)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"EXEC uspUpdateItem @itemID, @name, @price, @categoryID, @supplierID;";

                    #endregion

                    await db.ExecuteAsync(query, new
                    {
                        @itemID = item.ID,
                        @name = item.Name,
                        @price = item.Price,
                        @categoryID = item.Category.ID,
                        @supplierID = item.Inventory.Supplier.ID
                    },
                    commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task UpdateInventoryAsync(Item item)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"EXEC uspUpdateInventory @itemID, @supplierID, @quantity;";

                    #endregion

                    await db.ExecuteAsync(query, new
                    {
                        @itemID = item.ID,
                        @supplierID = item.Inventory.Supplier.ID,
                        @quantity = item.Inventory.Quantity
                    },
                    commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<CategoryData> GetCategoryAsync(int categoryID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"SELECT * FROM Category (NOLOCK) WHERE CategoryID = @categoryID;";

                    #endregion

                    return await db.QueryFirstOrDefaultAsync<CategoryData>(query, new { @categoryID = categoryID }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<CategoryData>> GetCategoryAsync()
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"SELECT * FROM Category (NOLOCK);";

                    #endregion

                    return await db.QueryAsync<CategoryData>(query, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<dynamic>> GetShortListOfItemsAsync()
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query =
                    @"  SELECT		*
                        FROM		Items			I  (NOLOCK)
                        INNER JOIN	Items_Inventory	II (NOLOCK)
                        	ON I.ItemID = II.ItemID
                        WHERE		I.Deleted  = 0
                          AND		II.Deleted = 0
                        ORDER BY    Name;";

                    #endregion

                    return await db.QueryAsync<dynamic>(query, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<int> GetItemQuantityAsync(int itemID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query =
                    @"  SELECT		Quantity
                        FROM		Items			AS I  (NOLOCK)
                        INNER JOIN	Items_Inventory AS IV (NOLOCK)
                        	ON I.ItemID = IV.ItemID
                        WHERE	I.ItemID = @itemID;";

                    #endregion

                    return await db.QueryFirstOrDefaultAsync<int>(query, new { @itemID = itemID }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }
    }
}
