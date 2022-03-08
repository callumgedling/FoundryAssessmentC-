using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using foundry_assessment_RAZOR.Model;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net.Http.Headers;

namespace foundry_assessment_RAZOR.API
{
    public class EmployeeAPI
    {
        readonly string baseUrl = "http://localhost:3000";
        readonly string clientsURL = "/clients/";
        readonly string employeesURL = "/employees/";
        readonly string engagementsURL = "/engagements/";


        public HttpStatusCode CreateEmployee(EmployeeName employeeName)
        {
            HttpClient httpClient = new HttpClient();
            string jsonInput = JsonConvert.SerializeObject(employeeName);
            var content = new StringContent(jsonInput, Encoding.UTF8, "application/json");
            var results = httpClient.PostAsync("http://localhost:3000/employees", content).Result;
            return results.StatusCode;
        }

        public List<EmployeeClass> ReadEmployees()
        {
            List<EmployeeClass> employeeList = new List<EmployeeClass>();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);

            var consumeAPI = httpClient.GetAsync("employees");
            consumeAPI.Wait();

            var readData = consumeAPI.Result;
            if (readData.IsSuccessStatusCode)
            {
                var jsonString = readData.Content.ReadAsStringAsync();
                employeeList = JsonConvert.DeserializeObject<List<EmployeeClass>>(jsonString.Result);
                Console.WriteLine(employeeList);
            }
            consumeAPI.Dispose();
            httpClient.Dispose();
            return employeeList;
        }
        public EmployeeClass FindEmployeeByID(string id)
        {
            EmployeeClass employee = new EmployeeClass();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);

            var consumeAPI = httpClient.GetAsync("employees/" + id);
            consumeAPI.Wait();

            var readData = consumeAPI.Result;
            if (readData.IsSuccessStatusCode)
            {
                var jsonString = readData.Content.ReadAsStringAsync();
                employee = JsonConvert.DeserializeObject<EmployeeClass>(jsonString.Result);
                Console.WriteLine(employee);
            }
            consumeAPI.Dispose();
            httpClient.Dispose();
            return employee;
        }

        public HttpStatusCode UpdateEmployee(EmployeeClass employee)
        {
            HttpClient httpClient = new HttpClient();
            EmployeeName employeeName = new EmployeeName();
            employeeName.name = employee.name;
            string jsonInput = JsonConvert.SerializeObject(employeeName);
            var content = new StringContent(jsonInput, Encoding.UTF8, "application/json");
            var results = httpClient.PutAsync(baseUrl + employeesURL + employee.id, content).Result;
            Console.WriteLine(results.StatusCode);
            httpClient.Dispose();
            return results.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteEmployee(string employeeId)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.DeleteAsync(baseUrl + employeesURL + employeeId);
            httpClient.Dispose();
            return response.StatusCode;
        }

    }
}
