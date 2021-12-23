using System;
using ERP.Store.Desktop.Repositories;
using ERP.Store.Desktop.Entities.JSON.Request;
using ERP.Store.Desktop.Entities.JSON.Response;

namespace ERP.Store.Desktop.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService()
        {
            _employeeRepository = new EmployeeRepository();
        }

        public int Post(EmployeeRequest employee, UserResponse user)
        {
            try
            {
                return _employeeRepository.Post(employee, user);
            }
            catch (Exception) { throw; }
        }

        public int Put(EmployeeRequest employee, UserResponse user)
        {
            try
            {
                return _employeeRepository.Put(employee, user);
            }
            catch (Exception) { throw; }
        }

        public EmployeeResponse Get(string identification, UserResponse user)
        {
            try
            {
                return _employeeRepository.Get(identification, user);
            }
            catch (Exception) { throw; }
        }

        public int Delete(string identification, UserResponse user)
        {
            try
            {
                return _employeeRepository.Delete(identification, user);
            }
            catch (Exception) { throw; }
        }
    }
}
