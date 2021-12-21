using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<EmployeeData> GetEmployeeAsync(string identification);

        Task InsertEmployeeAsync(Employee employee);

        Task UpdateEmployeeAsync(Employee employee);
    }
}
