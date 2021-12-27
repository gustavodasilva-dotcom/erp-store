using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface ISupplierRepository
    {
        Task<SupplierTable> GetSupplierAsync(string identification);

        Task InsertSupplierAsync(Supplier supplier);

        Task UpdateSupplierAsync(Supplier supplier);

        Task DeleteSupplierAsync(int supplierID);
    }
}
