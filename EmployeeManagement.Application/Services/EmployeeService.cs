using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using EmployeeManagement.DataAccess.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<EmployeeDto> GetEmployees()
        {
            //Get data from Repository
            var employee = _employeeRepository.GetEmployees();
            return null;
        }
        public EmployeeDto GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            return null;
        }

        public EmployeeDto InsertEmployee(EmployeeDto insertEmployee)
        {
            var result = _employeeRepository.InsertEmployee(insertEmployee);
            return null;
        }
        public EmployeeDto UpdateEmployee(EmployeeDto employee)
        {
            var result = _employeeRepository.UpdateDetails(employee);
            return null;
        }
        public EmployeeDto DeleteEmployee(int ID)
        {
            var result = _employeeRepository.DeleteEmployeeDetails(ID);
            return null;
        }

        
    }
}
