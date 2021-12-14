using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<ImageData> GetEmployeesImage(int employeeID);

        Task<int> InsertImageAsync(string base64);
    }
}
