using System.Text.Json;
using RestSharp;

namespace RealTime_Data_Analysis_Dashboard.Service;

public class CoinGeckoService
{
    private readonly ILogger<CoinGeckoService> _logger; 

    public CoinGeckoService(ILogger<CoinGeckoService> logger)
    {
        _logger = logger;
    }

    public async Task<double> GetBitcoinPrice()
    {
        var currentBitCoinPriceUrl = "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=usd";
        var historicalPriceBitCoinPriceUrl = new RestClientOptions("https://api.coingecko.com/api/v3/coins/bitcoin/history?date=30-12-2018");
        try
        {
            var options =
                new RestClientOptions(currentBitCoinPriceUrl);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-cg-demo-api-key", "CG-PpMWJzqerxppswJRyX7VXjNH ");
            var response = await client.GetAsync(request);

            if (response.Content is not null)
            {
                return ExtractPriceFromJson(response.Content);
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"HTTP Request Error: {ex.Message}"); // Log HTTP errors
            return 0.0; // Or throw exception
        }
        catch (JsonException ex)
        {
            _logger.LogError($"JSON Deserialization Error: {ex.Message}"); // Log JSON errors
            return 0.0; // Or throw exception
        }

        return 0.0;
    }

    private double ExtractPriceFromJson(string json)
    {
        using JsonDocument doc = JsonDocument.Parse(json);
        JsonElement root = doc.RootElement;
        return root.GetProperty("bitcoin").GetProperty("usd").GetDouble();
    }
}