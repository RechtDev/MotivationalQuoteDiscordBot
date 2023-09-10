using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProjectOedipus.Models.Responses;

namespace ProjectOedipus.Services
{
    internal class ZenQuoteService : IQuoteService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ZenQuoteService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<QuoteResponse> GetQuoteFromProvider()
        {
            try
            {
                var httpResponse = await _httpClient.GetAsync(this._httpClient.BaseAddress + $"/random" + "/" + _config.GetSection("AuthTokens")["ZenQuoteKey"]);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("GET request was not successful");
                }
                var response = await httpResponse.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<List<QuoteResponse>>(response);

                return responseObject[0];
            }
            catch (HttpRequestException ex)
            {

                throw;
            }

        }

        public async Task<List<QuoteResponse>> GetQuotesInSpecificCategory(string catergory)
        {
            try
            {
                var httpResponse = await _httpClient.GetAsync(this._httpClient.BaseAddress + $"/quotes" + "/" + _config.GetSection("AuthTokens")["ZenQuoteKey"] + $"?keyword={catergory}");
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("GET request was not successful");
                }
                var response = await httpResponse.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<List<QuoteResponse>>(response);

                return responseObject;
            }
            catch (HttpRequestException ex)
            {

                throw;
            }
        }
    }
}