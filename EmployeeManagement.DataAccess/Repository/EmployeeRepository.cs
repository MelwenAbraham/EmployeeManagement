using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.DataAccess.Repository
{
    /// <summary>
    /// Connect To Database and Perforum CRUD operations
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private SqlConnection _sqlConnection;

        public EmployeeRepository()
        {
            _sqlConnection = new SqlConnection("data source= (localdb)\\mssqllocaldb; database= trialWork;");
        }

        public IEnumerable<EmployeeData> GetEmployees()
        {
            try
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand(cmdText: "exec SelectAllEmployees", _sqlConnection);
                var sqlDataReader = sqlCommand.ExecuteReader();
                var listOfEmployee = new List<EmployeeData>();
                while (sqlDataReader.Read())
                {
                    listOfEmployee.Add(new EmployeeData()
                    {
                        Id = (int)sqlDataReader["ID"],
                        Name = (string)sqlDataReader["NAME"],
                        Department = (string)sqlDataReader["DEPARTMENT"]
                    });
                }
                return listOfEmployee;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
        public EmployeeData GetEmployeeById(int ID)
        {
            try
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand(cmdText: "exec GetById  @ID", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("ID", ID);
                var sqlDataReader = sqlCommand.ExecuteReader();
                var employee = new EmployeeData();
              
                while (sqlDataReader.Read())
                {
                    employee.Id = (int)sqlDataReader["ID"];
                    employee.Name = (string)sqlDataReader["NAME"];
                    employee.Department = (string)sqlDataReader["DEPARTMENT"];
                    employee.Age = (int)sqlDataReader["AGE"];
                    employee.Address = (string)sqlDataReader["ADDRESS"];
                }
                return employee;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
        public bool InsertEmployee (EmployeeData employee)
        {
            try
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand(cmdText: "EXEC InsertEmployee @NAME,@DEPARTMENT,@AGE,@ADDRESS", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("NAME", employee.Name);
                sqlCommand.Parameters.AddWithValue("DEPARTMENT", employee.Department);
                sqlCommand.Parameters.AddWithValue("AGE", employee.Age);
                sqlCommand.Parameters.AddWithValue("ADDRESS", employee.Address);

                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
        public bool UpdateDetails(EmployeeData employee)
        {
            try
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand(cmdText: "exec UpdateEmployee  @ID, @NAME, @DEPARTMENT, @AGE, @ADDRESS", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("ID", employee.Id);
                sqlCommand.Parameters.AddWithValue("NAME", employee.Name);
                sqlCommand.Parameters.AddWithValue("DEPARTMENT", employee.Department);
                sqlCommand.Parameters.AddWithValue("AGE", employee.Age);
                sqlCommand.Parameters.AddWithValue("ADDRESS", employee.Address);

                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
        public bool DeleteEmployeeDetails(int ID)
        {
            try
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand(cmdText: "exec DeleteById @ID", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("ID", ID);

                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
    }
}
