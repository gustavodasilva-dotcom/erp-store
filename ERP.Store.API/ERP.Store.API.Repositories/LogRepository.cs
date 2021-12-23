using Dapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ERP.Store.API.Repositories.Interfaces;

namespace ERP.Store.API.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly string _connectionString;

        public LogRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task LogAsync(string json, string message, string process)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"EXEC uspInsertLog @Json, @Message, @Process;";

                    #endregion

                    await db.ExecuteAsync(query, new { @Json = json, @Message = message, @Process = process }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task LogAsync(string json, string message, string process, string token, int id)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"EXEC uspInsertLog @Json, @Message, @Process, @Token, @ID;";

                    #endregion

                    await db.ExecuteAsync(query, new { @Json = json, @Message = message, @Process = process, @Token = token, @ID = id }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task LogAsync(string json, string message, string process, int id)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"EXEC uspInsertLog @Json, @Message, @Process, @ID;";

                    #endregion

                    await db.ExecuteAsync(query, new { @Json = json, @Message = message, @Process = process, @ID = id }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }
    }
}
