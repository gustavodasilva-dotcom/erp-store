using System.Threading.Tasks;
using ERP.Store.API.Entities.Models.ViewModel;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> LoginAsync(UserInputModel userInput);
    }
}
