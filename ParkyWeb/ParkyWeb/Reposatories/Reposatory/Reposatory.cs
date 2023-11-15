using Newtonsoft.Json;
using ParkyWeb.Reposatories.IReposatory;
using System.Text;
using System.Text.Json.Serialization;

namespace ParkyWeb.Reposatories.Reposatory
{
    public class Reposatory<T> : IReposatory<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public Reposatory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> CreateAsync(string url, T item)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if(item != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "Application/json");
                
            }
            else
            {
                return false;
            }
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if(response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string url, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url+id);
            var client =_httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;

        }

        public async Task<T> GetAsync(string url, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url +id);
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonObject = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonObject);
            }
            return null;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url );
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonObject = await response.Content.ReadAsStringAsync();
                return  JsonConvert.DeserializeObject<IEnumerable<T>>(jsonObject);
            }
            return null;
        }

        public async Task<bool> UpdateAsync(string url, T item)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, url);
            if (item != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "Application/json");

            }
            else
            {
                return false;
            }
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
