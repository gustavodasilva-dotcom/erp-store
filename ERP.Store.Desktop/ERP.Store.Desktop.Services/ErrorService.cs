using System;
using System.Collections.Generic;

namespace ERP.Store.Desktop.Services
{
    public class ErrorService
    {
        public void DeserializeErros(List<string> errors)
        {
            try
            {
                var errorConcat = string.Empty;

                foreach (var error in errors)
                {
                    errorConcat += error;
                }

                throw new Exception(errorConcat);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
