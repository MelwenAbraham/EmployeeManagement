using EmployeeManagement.API.Models;
using EmployeeManagement.Application.Contracts;
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
                /// get employee by calling GetEmployeeById() in IEmployeeService and store it in a variable and Map that variable to EmployeeDetailedViewModel. 
                var employeeDetailedViewModel = new EmployeeDetailedViewModel
                {
                    Id = 1,
                    Name = "Dummy Name",
                    Department = "Dummy Department",
                    Age = 30,
                    Address = "Dummy Address"
                };

                return Ok(employeeDetailedViewModel);
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
            /// get employees by calling GetEmployees() in IEmployeeService and store it in a variable and Map that variable to EmployeeDetailedViewModel. 
            /// 
            var listOfEmployeeViewModel = new List<EmployeeDetailedViewModel>()
            {
                new EmployeeDetailedViewModel
                {

                    Id = 1,
                    Name = "Dummy Name",
                    Department = "Dummy Department",
                    Age = 30,
                    Address = "Dummy Address"
                }
            };
            return Ok(listOfEmployeeViewModel);
        }

        //Create Employee Insert, Update and Delete Endpoint here
    }
}
