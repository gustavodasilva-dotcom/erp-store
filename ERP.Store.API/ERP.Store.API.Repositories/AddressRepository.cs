﻿using System;
using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
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

        public async Task<int> InsertAddressAsync(Address address)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @" EXEC uspInsertAddress @Zip, @Street, @Comment, @Neighborhood, @City, @State, @Country;";

                    #endregion SQL

                    return await db.ExecuteScalarAsync<int>(query, new 
                    { 
                       @Zip = address.Zip,
                       @Street = address.Street,
                       @Comment = address.Comment,
                       @Neighborhood = address.Neighborhood,
                       @City = address.City,
                       @State = address.State,
                       @Country = address.Country
                    },
                    commandTimeout: 30);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
