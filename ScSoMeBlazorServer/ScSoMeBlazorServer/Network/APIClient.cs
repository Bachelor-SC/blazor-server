using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ScSoMeBlazorServer.Network
{
   
    public class APIClient: IAPIClient
    {
        private readonly HttpClient client;

        public APIClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<string> getFromAPI(string path)
        {
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + path);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> postToAPI(string path,Object PostObject)
        {
            string jsonObject = JsonSerializer.Serialize(PostObject);
            HttpContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(client.BaseAddress + path,content);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> patchToAPI(string path, Object PostObject)
        {
            string jsonObject = JsonSerializer.Serialize(PostObject);
            HttpContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PatchAsync(client.BaseAddress + path, content);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> deleteFromAPI(string path)
        {
            
            HttpResponseMessage response = await client.DeleteAsync($"{client.BaseAddress}{path}");

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> deleteFromAPI(string path,Object PostObject)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"{client.BaseAddress}{path}");
            string jsonObject = JsonSerializer.Serialize(PostObject);

            HttpContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            requestMessage.Content = content;
            HttpResponseMessage response = await client.SendAsync(requestMessage);

            return await response.Content.ReadAsStringAsync();
        }


    }
}
