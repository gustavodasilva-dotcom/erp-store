using System;
using System.Threading.Tasks;
using ERP.Store.API.Entities.Entities;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Repositories.Interfaces;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task RegisterEmployeeAsync(EmployeeInputModel employeeInputModel)
        {
            try
            {
                var employeeInput = new Employee
                {

                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
