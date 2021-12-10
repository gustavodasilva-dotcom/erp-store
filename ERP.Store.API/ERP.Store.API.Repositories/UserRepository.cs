using System;
using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;
using Microsoft.Extensions.Configuration;
using ERP.Store.API.Repositories.Interfaces;

namespace ERP.Store.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<UserTable> CheckUserAsync(string username, string password)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    var query =
                    $@" IF (SELECT	1
                        	FROM	User_Info
                        	WHERE	Username = '{username}'
                        	  AND	DECRYPTBYPASSPHRASE('key', Password) = '{password}')
                        IS NOT NULL
                        BEGIN
                        
                        	SELECT		 E.EmployeeID
                        				,E.FirstName
                        				,E.MiddleName
                        				,E.LastName
                        				,UI.Username
                        				,E.Identification
                        				,J.Description
                        				,E.Access_LevelID
                        	FROM		User_Info		UI (NOLOCK)
                        	INNER JOIN	Employee		E  (NOLOCK) ON UI.User_InfoID = E.User_InfoID
                        	LEFT JOIN	Employee_Job	EJ (NOLOCK) ON EJ.EmployeeID = E.EmployeeID
                        	LEFT JOIN	Jobs			J  (NOLOCK) ON J.JobID = EJ.JobID
                        	WHERE	Username = '{username}'
                        	  AND	E.Deleted = 0;
                        
                        END;";

                    return await db.QueryFirstOrDefaultAsync<UserTable>(query, commandTimeout: 30);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
