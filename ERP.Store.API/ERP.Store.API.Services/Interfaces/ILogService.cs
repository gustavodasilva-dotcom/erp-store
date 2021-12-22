using System.Threading.Tasks;

namespace ERP.Store.API.Services.Interfaces
{
    public interface ILogService
    {
        Task LogAsync(object model, string message, string process);

        Task LogAsync(object model, string message, string process, string token, int id);

        Task LogAsync(object model, string message, string process, int id);
    }
}
