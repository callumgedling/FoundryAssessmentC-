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
    public class EngagementAPI
    {
        readonly string baseUrl = "http://localhost:3000";
        readonly string clientsURL = "/clients/";
        readonly string EmployeesURL = "/employees/";
        readonly string engagementsURL = "/engagements/";


        public List<EngagementDetails> Read()
        {
            List<EngagementDetails> engagementDetails = new List<EngagementDetails>();
            List<EngagementModel> tempList = new List<EngagementModel>();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);
            var consumeAPI = httpClient.GetAsync("engagements");
            consumeAPI.Wait();

            var readData = consumeAPI.Result;
            if (readData.IsSuccessStatusCode)
            {
                // Convert Json Data Input into C# Objects
                var jsonString = readData.Content.ReadAsStringAsync();
                tempList = JsonConvert.DeserializeObject<List<EngagementModel>>(jsonString.Result);
                Console.WriteLine(tempList);

                engagementDetails = GetDetails(tempList);
            }
            tempList.Clear();
            httpClient.Dispose();
            return engagementDetails;
        }

        public EngagementModel FindEngagementByID(string id)
        {
            EngagementModel engagment = new EngagementModel();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);
            var consumeAPI = httpClient.GetAsync("engagements/" + id);
            consumeAPI.Wait();

            var readData = consumeAPI.Result;
            if (readData.IsSuccessStatusCode)
            {
                // Convert Json Data Input into C# Objects
                var jsonString = readData.Content.ReadAsStringAsync();
                engagment = JsonConvert.DeserializeObject<EngagementModel>(jsonString.Result);
            }
            httpClient.Dispose();
            return engagment;
        }

        private List<EngagementDetails> GetDetails(List<EngagementModel> engagementModel)
        {
            List<EngagementDetails> engagementDetails = new List<EngagementDetails>();
            ClientAPI clientsApi = new ClientAPI();
            // Get list of client data
            var clientData = clientsApi.ReadClients();
            // Get list of Employee data
            EmployeeAPI empApi = new EmployeeAPI();
            var empData = empApi.ReadEmployees();
            // Loop through the engagements  
            foreach (var engagement in engagementModel)
            {
                EngagementDetails temp = new EngagementDetails();
                temp.id = engagement.id;
                temp.name = engagement.name;
                temp.description = engagement.description;
                temp.started = engagement.started;
                temp.ended = engagement.ended;
                foreach (var emp in empData)
                {
                    if (engagement.employee == emp.id)
                    {
                        temp.employee = emp;
                        break;
                    }
                }
                foreach (var client in clientData)
                {
                    if (engagement.client == client.id)
                    {
                        temp.client = client;
                        break;
                    }
                }
                engagementDetails.Add(temp);
            }
            return engagementDetails;
        }

        public HttpStatusCode Create(CreateEngagements engagements)
        {
            HttpClient httpClient = new HttpClient();
            string jsonInput = JsonConvert.SerializeObject(engagements);
            var content = new StringContent(jsonInput, Encoding.UTF8, "application/json");
            var results = httpClient.PostAsync(baseUrl + engagementsURL, content).Result;
            return results.StatusCode;
        }

        public void Update(UpdateEngagment details)
        {
            HttpClient httpClient = new HttpClient();

            string jsonInput = JsonConvert.SerializeObject(details);
            var content = new StringContent(jsonInput, Encoding.UTF8, "application/json");
            var results = httpClient.PutAsync(baseUrl + engagementsURL + details.id, content).Result;
            Console.WriteLine(results.StatusCode);

            // Kill http client
            httpClient.Dispose();
        }

        public async Task<HttpStatusCode> Delete(string id)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage responseMessage = await httpClient.DeleteAsync(baseUrl + engagementsURL + id);
            httpClient.Dispose();
            return responseMessage.StatusCode;
        }

    }
}
