using Dapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;
using ERP.Store.API.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ERP.Store.API.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly string _connectionString;

        public ClientRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<ClientData> GetClientAsync(string identification)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query =
                    @"  SELECT	*
                        FROM	Client (NOLOCK)
                        WHERE	Identification = @identification
                          AND	Deleted = 0;";

                    #endregion SQL

                    return await db.QueryFirstOrDefaultAsync<ClientData>(query, new { @identification = identification }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }

        public async Task InsertClientAsync(Client client)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    #region SQL

                    var query = @"EXEC uspRegisterClient
                                    @FirstName,
                                    @MiddleName,
                                    @LastName,
                                    @Identification,
                                    @AddressID,
                                    @ContactID,
                                    @ImageID;";

                    #endregion SQL

                    await db.ExecuteAsync(query, new
                    {
                        @FirstName = client.FirstName,
                        @MiddleName = client.MiddleName,
                        @LastName = client.LastName,
                        @Identification = client.Identification,
                        @AddressID = client.Address.ID,
                        @ContactID = client.Contact.ID,
                        @ImageID = client.Image.ID,
                    }, commandTimeout: 30);
                }
            }
            catch (Exception) { throw; }
        }
    }
}
