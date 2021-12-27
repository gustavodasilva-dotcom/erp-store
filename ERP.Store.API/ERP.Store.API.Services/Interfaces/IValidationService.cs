using System.Threading.Tasks;
using System.Collections.Generic;
using ERP.Store.API.Entities.Entities;
using ERP.Store.API.Entities.Models.ViewModel;

namespace ERP.Store.API.Services.Interfaces
{
    public interface IValidationService
    {
        Task<ErrorViewModel> InitializingReturn(IEnumerable<string> validations, int statusCode);

        Task<ErrorViewModel> InitializingReturn(string validation, int statusCode);

        Task<IEnumerable<string>> Validate(dynamic model, EntityType type);
    }
}
