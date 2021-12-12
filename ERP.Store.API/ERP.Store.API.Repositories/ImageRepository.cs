using System;
using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ERP.Store.API.Repositories.Interfaces;

namespace ERP.Store.API.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly string _connectionString;

        public ImageRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<int> InsertImageAsync(string base64)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"EXEC uspInsertImage @base64;";

                    #endregion SQL

                    return await db.ExecuteScalarAsync<int>(query, new { @base64 = base64 }, commandTimeout: 30);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
