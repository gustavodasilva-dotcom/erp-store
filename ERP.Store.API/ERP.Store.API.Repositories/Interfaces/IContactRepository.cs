using System.Threading.Tasks;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.Entities.Entities;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface IContactRepository
    {
        Task<ContactData> GetContactAsync(int contactID);

        Task<int> InsertContactAsync(Contact contact);

        Task UpdateContactAsync(Contact contact);

        Task DeleteContactAsync(int contactID);
    }
}
