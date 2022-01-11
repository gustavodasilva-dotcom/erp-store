using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<ClientData> GetClientAsync(string identification);

        Task<ClientData> GetClientAsync(int clientID);

        Task InsertClientAsync(Client client);

        Task UpdateClientAsync(Client client);

        Task DeleteClientAsync(int clientID);
    }
}
