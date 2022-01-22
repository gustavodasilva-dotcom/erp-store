using System.Collections.Generic;

namespace ERP.Store.API.Entities.Models.ViewModel
{
    public class ErrorViewModel
    {
        public int StatusCode { get; set; }

        public List<string> Messages { get; set; }
    }
}
