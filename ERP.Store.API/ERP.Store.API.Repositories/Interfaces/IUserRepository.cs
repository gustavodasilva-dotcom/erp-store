using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserInfoData> GetUserInfoAsync(int userInfoID);

        Task<UserData> CheckUserAsync(string username, string password);
    }
}
