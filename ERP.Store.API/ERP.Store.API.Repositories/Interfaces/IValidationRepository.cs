using System.Threading.Tasks;

namespace ERP.Store.API.Repositories.Interfaces
{
    public interface IValidationRepository
    {
        Task<bool> IsJob(int jobID);

        Task<bool> IsAccessLevel(int accessLevelID);
    }
}
