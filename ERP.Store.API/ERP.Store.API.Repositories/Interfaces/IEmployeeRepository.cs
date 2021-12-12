using System.Threading.Tasks;
using ERP.Store.API.Entities.Entities;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task InsertEmployeeAsync(Employee employee);
    }
}
