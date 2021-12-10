using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserTable> CheckUserAsync(string username, string password);
    }
}
