using System.Threading.Tasks;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<int> InsertImageAsync(string base64);
    }
}
