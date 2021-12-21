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
    public class ContactRepository : IContactRepository
    {
        private readonly string _connectionString;

        public ContactRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<ContactData> GetContactAsync(int contactID)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"SELECT * FROM Contact (NOLOCK) WHERE ContactID = @contactID AND Deleted = 0;";

                    #endregion SQL

                    return await db.QueryFirstOrDefaultAsync<ContactData>(query, new { @contactID = contactID }, commandTimeout: 30);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> InsertContactAsync(Contact contact)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"EXEC uspInsertContact @Email, @Cellphone, @Phone;";

                    #endregion SQL

                    return await db.ExecuteScalarAsync<int>(query, new
                    {
                        @Email = contact.Email,
                        @Cellphone = contact.Cellphone,
                        @Phone = contact.Phone
                    },
                    commandTimeout: 30);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"EXEC uspUpdateContact @ContactID, @Email, @Cellphone, @Phone;";

                    #endregion

                    await db.ExecuteAsync(query, new
                    {
                        @ContactID = contact.ID,
                        @Email = contact.Email,
                        @Cellphone = contact.Cellphone,
                        @Phone = contact.Phone
                    },
                    commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }
    }
}
