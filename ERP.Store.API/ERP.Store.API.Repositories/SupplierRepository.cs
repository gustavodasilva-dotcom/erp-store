using Dapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;
using ERP.Store.API.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ERP.Store.API.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly string _connectionString;

        public SupplierRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<SupplierTable> GetSupplierAsync(string identification)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query =
                    @"  SELECT	*
                        FROM	Supplier
                        WHERE	Identification = @identification
                          AND	Deleted = 0;";

                    #endregion

                    return await db.QueryFirstOrDefaultAsync<SupplierTable>(query, new { @identification = identification }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<SupplierTable> GetSupplierAsync(int supplierID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query =
                    @"  SELECT	*
                        FROM	Supplier
                        WHERE	SupplierID = @supplierID
                          AND   Deleted = 0;";

                    #endregion

                    return await db.QueryFirstOrDefaultAsync<SupplierTable>(query, new { @supplierID = supplierID }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task InsertSupplierAsync(Supplier supplier)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"EXEC uspRegisterSupplier @Name, @Identification, @AddressID, @ContactID;";

                    #endregion

                    await db.ExecuteAsync(query, new
                    {
                        @Name = supplier.Name,
                        @Identification = supplier.Identification,
                        @AddressID = supplier.Address.ID,
                        @ContactID = supplier.Contact.ID
                    },
                    commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"EXEC uspUpdateSupplier @SupplierID, @Name, @Identification;";

                    #endregion

                    await db.ExecuteAsync(query, new
                    {
                        @SupplierID = supplier.ID,
                        @Name = supplier.Name,
                        @Identification = supplier.Identification
                    },
                    commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task DeleteSupplierAsync(int supplierID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query =
                    @"  UPDATE	Supplier
                        SET
                        	Deleted = 1
                        WHERE	SupplierID = @supplierID;";

                    #endregion

                    await db.ExecuteAsync(query, new { @supplierID = supplierID }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }
    }
}
