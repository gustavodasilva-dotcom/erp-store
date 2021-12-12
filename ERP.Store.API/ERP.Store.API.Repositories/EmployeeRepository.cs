using System;
using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
