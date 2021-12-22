using System.Threading.Tasks;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task LogAsync(string json, string message, string process);

        Task LogAsync(string json, string message, string process, string token, int id);

        Task LogAsync(string json, string message, string process, int id);
    }
}
