using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Repositories.Interfaces;

namespace ERP.Store.API.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task LogAsync(object model, string message, string process)
        {
            try
            {
                await _logRepository.LogAsync(ConvertModelToJason(model), message, process);
            }
            catch (Exception e)
            {
                await _logRepository.LogAsync(ConvertModelToJason(model), e.Message, "LogAsync() : LogService");

                throw;
            }
        }

        public async Task LogAsync(object model, string message, string process, string token, int id)
        {
            try
            {
                await _logRepository.LogAsync(ConvertModelToJason(model), message, process, token, id);
            }
            catch (Exception e)
            {
                await _logRepository.LogAsync(ConvertModelToJason(model), e.Message, "LogAsync() : LogService");

                throw;
            }
        }

        public async Task LogAsync(object model, string message, string process, int id)
        {
            try
            {
                await _logRepository.LogAsync(ConvertModelToJason(model), message, process, id);
            }
            catch (Exception e)
            {
                await _logRepository.LogAsync(ConvertModelToJason(model), e.Message, "LogAsync() : LogService");

                throw;
            }
        }

        private static string ConvertModelToJason(object model)
        {
            try
            {
                return JsonConvert.SerializeObject(model);
            }
            catch (Exception) { throw; }
        }
    }
}
