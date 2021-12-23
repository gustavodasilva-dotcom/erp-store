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

        public async Task<UserInfoData> GetUserInfoAsync(int userInfoID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query =
                    @" SELECT	 User_InfoID
                        		,Username
                        		,CAST(DECRYPTBYPASSPHRASE('key', Password) AS VARCHAR) AS Password
                        		,Deleted
                        		,InsertDate
                        FROM	User_Info (NOLOCK)
                        WHERE	User_InfoID = @userInfoID
                          AND	Deleted     = 0;";

                    #endregion SQL

                    return await db.QueryFirstOrDefaultAsync<UserInfoData>(query, new { @userInfoID = userInfoID }, commandTimeout: 30);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserData> CheckUserAsync(string username, string password)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query =
                    $@" SELECT		 E.EmployeeID
	                    			,E.FirstName
	                    			,E.MiddleName
	                    			,E.LastName
	                    			,UI.Username
	                    			,E.Identification
	                    			,J.Description
	                    			,E.Access_LevelID
	                    FROM		User_Info		UI (NOLOCK)
	                    INNER JOIN	Employee		E  (NOLOCK) ON UI.User_InfoID = E.User_InfoID
	                    LEFT JOIN	Employee_Job	EJ (NOLOCK) ON EJ.EmployeeID  = E.EmployeeID
	                    LEFT JOIN	Jobs			J  (NOLOCK) ON J.JobID		  = EJ.JobID
	                    WHERE		UI.Username = @username
	                      AND		CAST(DECRYPTBYPASSPHRASE('key', UI.Password) AS VARCHAR) = @password;";

                    #endregion SQL

                    return await db.QueryFirstOrDefaultAsync<UserData>(query, new { @username = username, @password = password }, commandTimeout: 30);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
