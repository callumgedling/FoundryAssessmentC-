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
        readonly string employeesURL = "/employees/";
        readonly string engagementsURL = "/engagements/";


        public HttpStatusCode CreateClient(ClientName clientName)
        {
            HttpClient httpClient = new HttpClient();
            string jsonInput = JsonConvert.SerializeObject(clientName);
            var content = new StringContent(jsonInput, Encoding.UTF8, "application/json");
            var results = httpClient.PostAsync("http://localhost:3000/clients", content).Result;
            return results.StatusCode;
        }

        public List<ClientClass> ReadClients()
        {
            List<ClientClass> clientList = new List<ClientClass>();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);

            var consumeAPI = httpClient.GetAsync("clients");
            consumeAPI.Wait();

            var readData = consumeAPI.Result;
            if (readData.IsSuccessStatusCode)
            {
                var jsonString = readData.Content.ReadAsStringAsync();
                clientList = JsonConvert.DeserializeObject<List<ClientClass>>(jsonString.Result);
                Console.WriteLine(clientList);
            }
            consumeAPI.Dispose();
            httpClient.Dispose();
            return clientList;
        }
        public ClientClass FindClientByID(string id)
        {
            ClientClass client = new ClientClass();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);

            var consumeAPI = httpClient.GetAsync("clients/" + id);
            consumeAPI.Wait();

            var readData = consumeAPI.Result;
            if (readData.IsSuccessStatusCode)
            {
                var jsonString = readData.Content.ReadAsStringAsync();
                client = JsonConvert.DeserializeObject<ClientClass>(jsonString.Result);
                Console.WriteLine(client);
            }
            consumeAPI.Dispose();
            httpClient.Dispose();
            return client;
        }

        public HttpStatusCode UpdateClient(ClientClass client)
        {
            HttpClient httpClient = new HttpClient();
            ClientName clientName = new ClientName();
            clientName.name = client.name;
            string jsonInput = JsonConvert.SerializeObject(clientName);
            var content = new StringContent(jsonInput, Encoding.UTF8, "application/json");
            var results = httpClient.PutAsync(baseUrl + clientsURL + client.id, content).Result;
            Console.WriteLine(results.StatusCode);
            httpClient.Dispose();
            return results.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteClient(string clientId)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.DeleteAsync(baseUrl + clientsURL + clientId);
            httpClient.Dispose();
            return response.StatusCode;
        }

    }
}
