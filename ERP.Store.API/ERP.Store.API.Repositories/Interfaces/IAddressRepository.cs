using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task<AddressData> GetAddressAsync(int addressID);

        Task<int> InsertAddressAsync(Address address);
    }
}
