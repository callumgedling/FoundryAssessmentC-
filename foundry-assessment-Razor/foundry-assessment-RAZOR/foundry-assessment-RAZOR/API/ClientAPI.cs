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
    public class ClientAPI
    {
        readonly string baseUrl = "http://localhost:3000";
        readonly string clientsURL = "/clients/";
        readonly string EmployeesURL = "/employees/";
        readonly string engagementsURL = "/engagements/";

        public HttpStatusCode CreateClient(ClientName clientName)
        {
            HttpClient httpClient = new HttpClient();
            string jsonInput = JsonConvert.SerializeObject(clientName);
            var content = new StringContent(jsonInput, Encoding.UTF8, "application/json");
            var results = httpClient.PostAsync(baseUrl + clientsURL, content).Result;
            return results.StatusCode;
        }

        public List<ClientClass> ReadClients()
        {
            List<ClientClass> ClientsList = new List<ClientClass>();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:3000/");

            var consumeAPI = httpClient.GetAsync("clients");
            consumeAPI.Wait();

            var readData = consumeAPI.Result;
            if (readData.IsSuccessStatusCode)
            {
                var jsonString = readData.Content.ReadAsStringAsync();
                ClientsList = JsonConvert.DeserializeObject<List<ClientClass>>(jsonString.Result);
                Console.WriteLine(ClientsList);
            }
            consumeAPI.Dispose();
            httpClient.Dispose();
            return ClientsList;
        }

    }
}
