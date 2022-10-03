using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using System;
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
            try
            {
                var returnedListOfEmployees = _employeeRepository.GetEmployees();
                return MaptoListOfEmployeeDto(returnedListOfEmployees);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private IEnumerable<EmployeeDto> MaptoListOfEmployeeDto(IEnumerable<EmployeeData> returnedListOfEmployees)
        {
            var listOfEmployeeDto = new List<EmployeeDto>();
            foreach (var item in returnedListOfEmployees)
            {
                var employeeDto = new EmployeeDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Department = item.Department,
                    Age = item.Age,
                    Address = item.Address
                };
                listOfEmployeeDto.Add(employeeDto);
            }
            return listOfEmployeeDto;
        }

        public EmployeeDto GetEmployeeById(int id)
        {
            try
            {
                var returnedEmployee = _employeeRepository.GetEmployeeById(id);
                return MapToEmployeeDto(returnedEmployee);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private EmployeeDto MapToEmployeeDto(EmployeeData employee)
        {
           return new EmployeeDto()
           {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Age = employee.Age,
                Address = employee.Address
           };
        }

        public bool InsertEmployee(EmployeeDto employee)
        {
            try
            {
                var employeeData = new EmployeeData()
                {
                    //Id = employee.Id,
                    Name = employee.Name,
                    Department = employee.Department,
                    Age = employee.Age,
                    Address = employee.Address
                };
                _employeeRepository.InsertEmployee(employeeData);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public bool UpdateEmployee(EmployeeDto employee)
        {
            var employeeData = new EmployeeData()
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Age = employee.Age,
                Address = employee.Address
            };
            _employeeRepository.UpdateDetails(employeeData);
            return true;
        }

        public bool DeleteEmployee(int ID)
        {
            _employeeRepository.DeleteEmployeeDetails(ID);
            return true;
        }
    }
}
