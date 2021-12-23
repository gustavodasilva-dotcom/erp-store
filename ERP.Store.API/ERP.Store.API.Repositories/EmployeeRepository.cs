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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<EmployeeData> GetEmployeeAsync(string identification)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query =
                    @"  SELECT		 E.EmployeeID
			                        ,FirstName
			                        ,MiddleName
			                        ,LastName
			                        ,Identification
			                        ,Access_LevelID
			                        ,User_InfoID
			                        ,ContactID
			                        ,AddressID
			                        ,Salary
			                        ,J.JobID
			                        ,Description
                        FROM		Employee		E  (NOLOCK)
                        INNER JOIN	Employee_Job	EJ (NOLOCK)	ON EJ.EmployeeID	= E.EmployeeID
                        INNER JOIN	Jobs			J  (NOLOCK)	ON J.JobID			= EJ.JobID
                        WHERE		E.Identification = @identification
                          AND		E.Deleted		 = 0
                          AND       EJ.Deleted       = 0
                          AND       J.Deleted        = 0;";

                    #endregion SQL

                    return await db.QueryFirstOrDefaultAsync<EmployeeData>(query, new { @identification = identification }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task InsertEmployeeAsync(Employee employee)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"EXEC uspRegisterEmployee
                                    @FirstName,
                                    @MiddleName,
                                    @LastName,
                                    @Identification,
                                    @Username,
                                    @Password,
                                    @AddressID,
                                    @ContactID,
                                    @ImageID,
                                    @Access_LevelID,
                                    @Salary,
                                    @JobID;";

                    #endregion SQL

                    await db.ExecuteAsync(query, new
                    {
                        @FirstName = employee.FirstName,
                        @MiddleName = employee.MiddleName,
                        @LastName = employee.LastName,
                        @Identification = employee.Identification,
                        @Username = employee.User.Username,
                        @Password = employee.User.Password,
                        @AddressID = employee.Address.ID,
                        @ContactID = employee.Contact.ID,
                        @ImageID = employee.Image.ID,
                        @Access_LevelID = employee.ExtraInfo.AccessLevelID,
                        @Salary = employee.ExtraInfo.Salary,
                        @JobID = employee.ExtraInfo.JobID
                    }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"EXEC uspUpdateEmployee
                                  @EmployeeID,
                                  @Access_LevelID,
                                  @JobID,
                                  @User_InfoID,
                                  @FirstName,
                                  @MiddleName,
                                  @LastName,
                                  @Identification,
                                  @Username,
                                  @Password,
                                  @Salary;";

                    #endregion

                    await db.ExecuteAsync(query, new
                    {
                        @EmployeeID = employee.ID,
                        @Access_LevelID = employee.ExtraInfo.AccessLevelID,
                        @JobID = employee.ExtraInfo.JobID,
                        @User_InfoID = employee.User.ID,
                        @FirstName = employee.FirstName,
                        @MiddleName = employee.MiddleName,
                        @LastName = employee.LastName,
                        @Identification = employee.Identification,
                        @Username = employee.User.Username,
                        @Password = employee.User.Password,
                        @Salary = employee.ExtraInfo.Salary,
                        
                    }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task DeleteEmployeeAsync(int employeeID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query =
                    @"  UPDATE	Employee
                        SET
                        	Deleted = 1
                        WHERE	EmployeeID = @employeeID;";

                    #endregion

                    await db.ExecuteAsync(query, new { @employeeID = employeeID }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }
    }
}
