using ERP.Store.API.Entities.Models.ViewModel;

namespace ERP.Store.API.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserViewModel user);
    }
}
