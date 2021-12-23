using System.Threading.Tasks;
using ERP.Store.API.Entities.Models.ViewModel;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeViewModel> GetEmployeeAsync(string identification);

        Task RegisterEmployeeAsync(EmployeeInputModel input);

        Task UpdateEmployeeAsync(EmployeeInputModel input);

        Task<bool> DeleteEmployeeAsync(string identification);
    }
}
