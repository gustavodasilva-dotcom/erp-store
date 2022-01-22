using System.Threading.Tasks;
using ERP.Store.API.Entities.Models.ViewModel;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Services.Interfaces
{
    public interface IClientService
    {
        Task<ClientViewModel> GetClientAsync(string identification);

        Task<ClientViewModel> GetClientAsync(int clientID);

        Task RegisterClientAsync(ClientInputModel input);

        Task UpdateClientAsync(ClientInputModel input);

        Task<bool> DeleteClientAsync(string identification);
    }
}
