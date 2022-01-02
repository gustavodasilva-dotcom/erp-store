using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<ImageData> GetEmployeesImage(int employeeID);

        Task<ImageData> GetClientsImage(int clientID);

        Task<ImageData> GetItemsImageAsync(int itemID);

        Task<int> InsertImageAsync(string base64);

        Task UpdateImageAsync(Image image);

        Task DeleteImageAsync(int imageID);
    }
}
