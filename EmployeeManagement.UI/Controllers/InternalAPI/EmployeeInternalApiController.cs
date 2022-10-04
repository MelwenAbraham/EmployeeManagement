using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Providers.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.UI.Controllers.InternalAPI
{
    [Route("api/internal/employee")]
    [ApiController]
    public class EmployeeInternalApiController : ControllerBase
    {
        private readonly IEmployeeApiClient _employeeApiClient;

        public EmployeeInternalApiController(IEmployeeApiClient employeeApiClient)
        {
            _employeeApiClient = employeeApiClient;
        }

        [HttpGet]
        [Route("{employeeId}")]
        public IActionResult GetEmployeeById([FromRoute] int employeeId)
        {
            try
            {
                var employee = _employeeApiClient.GetEmployeeById(employeeId);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete]
        [Route("deleteemployees/{employeeId}")]
        public IActionResult DeleteEmployee([FromRoute]int employeeId)
        {
            try
            {
                var deleted = _employeeApiClient.DeleteEmployee(employeeId);
                return Ok(deleted);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("insertemployees")]
        public IActionResult InsertEmployee([FromBody] EmployeeDetailedViewModel employee)
        {
            try
            {
                var insertedEmployee = _employeeApiClient.InsertEmployee(employee);
                return Ok(insertedEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Route("updateemployees")]
        public IActionResult UpdateEmployee(EmployeeDetailedViewModel employee)
        {
            try
            {
                var updatedEmployee = _employeeApiClient.UpdateEmployee(employee);
                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
