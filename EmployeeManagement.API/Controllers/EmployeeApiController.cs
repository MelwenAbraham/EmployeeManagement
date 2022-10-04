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

        private object MapToemployeeById(Application.Models.EmployeeDto employeeById)
        {
            var employee = new EmployeeDetailedViewModel()
            {
                Id = employeeById.Id,
                Name = employeeById.Name,
                Department = employeeById.Department,
                Age = employeeById.Age,
                Address = employeeById.Address
            };
            return employee;
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

        private object MapToAllEmployee(IEnumerable<Application.Models.EmployeeDto> listOfEmployeeViewModel)
        {
            var listOfAllEmployee = new List<EmployeeDetailedViewModel>();
            foreach (var item in listOfEmployeeViewModel)
            {
                var employeeViewModel = new EmployeeDetailedViewModel()
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
                var employeeDetailed = new EmployeeDto()
                {
                    Name = newEmployee.Name,
                    Department = newEmployee.Department,
                    Age = newEmployee.Age,
                    Address = newEmployee.Address
                };
                var employeeDetailedViewModel = _employeeService.InsertEmployee(employeeDetailed);
                return Ok(employeeDetailedViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("updateemployees")]
        public IActionResult UpdateEmployee([FromBody] EmployeeDetailedViewModel newEmployee)
        {
            try
            {
                var employeeDto = new EmployeeDto()
                {
                    Id = newEmployee.Id,
                    Name = newEmployee.Name,
                    Department = newEmployee.Department,
                    Age = newEmployee.Age,
                    Address = newEmployee.Address
                };
                var employeeDetailedViewModel = _employeeService.UpdateEmployee(employeeDto);
                return Ok(employeeDetailedViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
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
