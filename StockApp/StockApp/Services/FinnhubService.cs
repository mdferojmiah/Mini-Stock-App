using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;
using StockApp.ServiceContracts;

namespace StockApp.Services;

public class FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IFinnhubService
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IConfiguration _configuration = configuration;

    public async Task<Dictionary<string, object>?> GetStockPrice(string symbol)
    {
        using (HttpClient httpClient = _httpClientFactory.CreateClient())
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={symbol}&token={_configuration.GetValue<string>("FinnhubToken")}"),
                Method = HttpMethod.Get
            };
            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();
            StreamReader streamReader = new StreamReader(stream);
            string response = await streamReader.ReadToEndAsync();
            Dictionary<string, object>? dictionaryResponse =JsonSerializer.Deserialize<Dictionary<string, object>?>(response);
            if (dictionaryResponse == null)
            {
                throw new InvalidOperationException("No response From Finnhub");
            }
            if (dictionaryResponse.TryGetValue("error", out var value))
            {
                throw new InvalidOperationException(Convert.ToString(value));
            }
            return dictionaryResponse;
        }

        
    }
}