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


    }
}
