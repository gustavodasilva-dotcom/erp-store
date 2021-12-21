using System;
using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;
using Microsoft.Extensions.Configuration;
using ERP.Store.API.Repositories.Interfaces;

namespace ERP.Store.API.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly string _connectionString;

        public AddressRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<AddressData> GetAddressAsync(int addressID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"SELECT * FROM Address (NOLOCK) WHERE AddressID = @addressID AND Deleted = 0;";

                    #endregion SQL

                    return await db.QueryFirstOrDefaultAsync<AddressData>(query, new { @addressID = addressID }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<int> InsertAddressAsync(Address address)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @" EXEC uspInsertAddress @Zip, @Street, @Number, @Comment, @Neighborhood, @City, @State, @Country;";

                    #endregion SQL

                    return await db.ExecuteScalarAsync<int>(query, new 
                    { 
                       @Zip = address.Zip,
                       @Street = address.Street,
                       @Number = address.Number,
                       @Comment = address.Comment,
                       @Neighborhood = address.Neighborhood,
                       @City = address.City,
                       @State = address.State,
                       @Country = address.Country
                    },
                    commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task UpdateAddressAsync(Address address)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"EXEC uspUpdateAddress @AddressID, @Zip, @Street, @Number, @Comment, @Neighborhood, @City, @State, @Country;";

                    #endregion

                    await db.ExecuteAsync(query, new
                    {
                        @AddressID = address.ID,
                        @Zip = address.Zip,
                        @Street = address.Street,
                        @Number = address.Number,
                        @Comment = address.Comment,
                        @Neighborhood = address.Neighborhood,
                        @City = address.City,
                        @State = address.State,
                        @Country = address.Country
                    },
                    commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }
    }
}
