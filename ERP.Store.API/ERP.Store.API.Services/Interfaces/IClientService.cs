using System.Threading.Tasks;
using ERP.Store.API.Entities.Models.ViewModel;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Services.Interfaces
{
    public interface IClientService
    {
        Task<ClientViewModel> GetClientAsync(string identification);

        Task RegisterClientAsync(ClientInputModel input);
    }
}
