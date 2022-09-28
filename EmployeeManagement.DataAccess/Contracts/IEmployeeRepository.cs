using EmployeeManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.DataAccess.Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeData> GetEmployees();
        EmployeeData GetEmployeeById(int id);
        public bool InsertEmployee(EmployeeData employee);
        public bool UpdateDetails(EmployeeData employee);
        public bool DeleteEmployeeDetails(int ID);
    }
}
