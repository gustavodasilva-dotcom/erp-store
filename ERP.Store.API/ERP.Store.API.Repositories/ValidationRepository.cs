using Dapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ERP.Store.API.Repositories.Interfaces;

namespace ERP.Store.API.Repositories
{
    public class ValidationRepository : IValidationRepository
    {
        private readonly string _connectionString;

        public ValidationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<bool> IsJob(int jobID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"SELECT 1 FROM Jobs (NOLOCK) WHERE JobID = @jobID;";

                    #endregion

                    return await db.QueryFirstOrDefaultAsync<bool>(query, new { @jobID = jobID }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> IsAccessLevel(int accessLevelID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"SELECT 1 FROM Access_Level (NOLOCK) WHERE Access_LevelID = @accessLevelID;";

                    #endregion

                    return await db.QueryFirstOrDefaultAsync<bool>(query, new { @accessLevelID = accessLevelID }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }
    }
}
