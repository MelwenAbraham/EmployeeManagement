using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Models.Provider;
using EmployeeManagement.UI.Providers.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EmployeeManagement.UI.Providers.ApiClients
{
    public class EmployeeApiClient : IEmployeeApiClient
    {
        private readonly HttpClient _httpClient;

        public EmployeeApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IEnumerable<EmployeeViewModel> GetAllEmployee()
        {
            //Consume /employee endpoint in the EmployeeManagementApi using _httpClient 
            using var response = _httpClient.GetAsync("https://localhost:5001/api/employee/get-all").Result;
            {
                var listOfEmployee = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(response.Content.ReadAsStringAsync().Result);
                return listOfEmployee;
            }
        }

        public EmployeeDetailedViewModel GetEmployeeById(int employeeId)
        {
            var response = _httpClient.GetAsync("https://localhost:5001/api/employee/"+employeeId).Result;
            var employee = JsonConvert.DeserializeObject<EmployeeDetailedViewModel>(response.Content.ReadAsStringAsync().Result);
            return employee;
        }

        public bool DeleteEmployee(int employeeId)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(employeeId));
            using var response = _httpClient.DeleteAsync("https://localhost:5001/api/employee/deleteEmployee/" + employeeId).Result;
            return true;
        }
        public bool InsertEmployee(EmployeeDetailedViewModel employee)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
            using var response = _httpClient.PostAsync("https://localhost:5001/api/employee/insertEmployees", stringContent).Result;
            {
                response.Content.ReadAsStringAsync();
                return true;
            }
        }
        public bool UpdateEmployee(EmployeeDetailedViewModel employee)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
            using (var response = _httpClient.PutAsync("https://localhost:5001/api/employee/updateemployees", stringContent).Result)
            {
                response.Content.ReadAsStringAsync();
                return true;
            }
        }
    }
}
