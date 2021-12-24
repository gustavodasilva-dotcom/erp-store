using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<ClientData> GetClientAsync(string identification);

        Task InsertClientAsync(Client client);
    }
}
