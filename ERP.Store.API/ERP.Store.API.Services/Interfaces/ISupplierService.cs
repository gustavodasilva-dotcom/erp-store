using System.Threading.Tasks;
using ERP.Store.API.Entities.Models.ViewModel;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<SupplierViewModel> GetSupplierAsync(string identification);

        Task RegisterSupplierAsync(SupplierInputModel input);

        Task UpdateSupplierAsync(SupplierInputModel input);

        Task<bool> DeleteSupplierAsync(string identification);
    }
}
