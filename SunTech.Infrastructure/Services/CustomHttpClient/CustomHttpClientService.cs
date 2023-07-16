using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SunTech.Infrastructure.Services.CustomHttpClient
{
    public class CustomHttpClientService : ICustomHttpClientService
    {
        private readonly string _uri;
        private readonly HttpClient _client;

        public CustomHttpClientService(string uri)
        {
            _uri = uri;

            if (_client == null)
            {
                _client = new HttpClient();
            }
        }


        public async Task SendMessageToEventGrid(string subject, string verb, dynamic data)
        {

            using StringContent jsonContent = new StringContent(
               JsonConvert.SerializeObject(new
               {
                   Subject = subject,
                   verb = verb,
                   Data = data,
               }),
               Encoding.UTF8,
               "application/json");

            await _client.PostAsync(_uri, jsonContent);
        }
    }
}
