using System;
using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;
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

        public async Task<ImageData> GetEmployeesImage(int employeeID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query =
                    @"  SELECT		 I.ImageID
                        			,I.Base64
                        			,I.Deleted
                        			,I.InsertDate
                        FROM		Employee_Image	EI	(NOLOCK)
                        INNER JOIN	Image			I	(NOLOCK) ON EI.ImageID = I.ImageID
                        WHERE		EI.EmployeeID = @employeeID
                          AND		EI.Deleted = 0
                          AND		I.Deleted  = 0;";

                    #endregion SQL

                    return await db.QueryFirstOrDefaultAsync<ImageData>(query, new { @employeeID = employeeID }, commandTimeout: 30);
                }
            }
            catch (Exception)
            {
                throw;
            }
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
