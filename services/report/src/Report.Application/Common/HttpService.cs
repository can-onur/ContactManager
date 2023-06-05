using System.Text;
using Newtonsoft.Json;

namespace Report.Application.Common
{
    public class JsonSerializerHelper
    {
        public static async Task<T> Deserialize<T>(string json)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(json));
        }

        public static async Task<string> Serialize<T>(T obj)
        {
            return await Task.Run(() => JsonConvert.SerializeObject(obj));
        }
    }

    public class HttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> Get<T>(string url)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return await JsonSerializerHelper.Deserialize<T>(responseBody);
            }

            throw new Exception($"HTTP request failed with status code: {response.StatusCode}");
        }

        public async Task<T> Post<T>(string url, T requestBody)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            string requestBodyJson = await JsonSerializerHelper.Serialize(requestBody);
            HttpResponseMessage response = await httpClient.PostAsync(url, new StringContent(requestBodyJson, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return await JsonSerializerHelper.Deserialize<T>(responseBody);
            }

            throw new Exception($"HTTP request failed with status code: {response.StatusCode}");
        }
    }
}
