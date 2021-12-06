using Newtonsoft.Json;
using ProductAPP.BLLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPP.BLLayer.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        public HttpService()
        {
            _httpClient = new HttpClient();
        }

        public Task<T> Delete<T>(string uri)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Get<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await SendRequest<T>(request);
            return response;

        }

        public async Task<T> Post<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            var response = await SendRequest<T>(request);
            return response;
        }

        public Task<T> Put<T>(string uri, object value)
        {
            throw new NotImplementedException();
        }

        private async Task<T> SendRequest<T>(HttpRequestMessage request)
        {
            using var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            var responseStr = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<T>(responseStr);

            return apiResponse;
        }
    }
}
