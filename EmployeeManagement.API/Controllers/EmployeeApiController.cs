using EmployeeManagement.API.Models;
using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeApiController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        [HttpGet]
        [Route("{employeeId}")]
        public IActionResult GetEmployeeById([FromRoute] int employeeId)
        {
            try
            {
                var employeeById = _employeeService.GetEmployeeById(employeeId);
                return Ok(MapToemployeeById(employeeById));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private EmployeeDetailedViewModel MapToemployeeById(EmployeeDto employees)
        {
            try
            {
                var employee = new EmployeeDetailedViewModel
                {
                    Id = employees.Id,
                    Name = employees.Name,
                    Department = employees.Department,
                    Age = employees.Age,
                    Address = employees.Address
                };
                return employee;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("get-all")]
        public IActionResult GetEmployees()
        {
            try
            {
                var listOfEmployeeViewModel = _employeeService.GetEmployees();
                return Ok(MapToAllEmployee(listOfEmployeeViewModel));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private IEnumerable<EmployeeDetailedViewModel> MapToAllEmployee(IEnumerable<EmployeeDto> listOfEmployees)
        {
            var listOfAllEmployee = new List<EmployeeDetailedViewModel>();
            foreach (var item in listOfEmployees)
            {
                var employeeViewModel = new EmployeeDetailedViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Department = item.Department,
                    Age = item.Age,
                    Address = item.Address
                };
                listOfAllEmployee.Add(employeeViewModel);
            }
            return listOfAllEmployee;
        }

        [HttpPost]
        [Route("insertemployees")]
        public IActionResult InsertEmployee([FromBody] EmployeeDetailedViewModel newEmployee)
        {
            try
            {
                var insertedEmployee = _employeeService.InsertEmployee(MaptoInsertEmployee(newEmployee));
                return Ok(insertedEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private EmployeeDto MaptoInsertEmployee(EmployeeDetailedViewModel insertEmployee)
        {
            var employeeInsertion = new EmployeeDto()
            {
                Name = insertEmployee.Name,
                Age = insertEmployee.Age,
                Department = insertEmployee.Department,
                Address = insertEmployee.Address
            };
            return employeeInsertion;
        }

        [HttpPut]
        [Route("updateemployees")]
        public IActionResult UpdateEmployee([FromBody] EmployeeDetailedViewModel updateEmployee)
        {
            try
            {
                var updatedEmployee = _employeeService.UpdateEmployee(MapToUpdateEmployee(updateEmployee));
                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private EmployeeDto MapToUpdateEmployee(EmployeeDetailedViewModel updateEmployee)
        {
            var employeeUpdation = new EmployeeDto();
            {
                employeeUpdation.Id = updateEmployee.Id;
                employeeUpdation.Name = updateEmployee.Name;
                employeeUpdation.Department = updateEmployee.Department;
                employeeUpdation.Age = updateEmployee.Age;
                employeeUpdation.Address = updateEmployee.Address;
            };
            return employeeUpdation;
        }

        [HttpDelete]
        [Route("deleteEmployee/{ID}")]
        public IActionResult DeleteEmployee(int ID)
        {
            var employeeDetailedViewModel = _employeeService.DeleteEmployee(ID);
            return Ok(employeeDetailedViewModel);
        }
    }
}
