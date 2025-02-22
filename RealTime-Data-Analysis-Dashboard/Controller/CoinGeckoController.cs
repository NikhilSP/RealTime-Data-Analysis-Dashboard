using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RealTime_Data_Analysis_Dashboard.Service;

namespace RealTime_Data_Analysis_Dashboard.Controller;

[ApiController]
[Route("api/[controller]")] // Route will be /api/coinGecko
public class CoinGeckoController
{
    private readonly CoinGeckoService _coinGeckoService;
    private readonly ILogger<CoinGeckoController> _logger;

    public CoinGeckoController(CoinGeckoService coinGeckoService, ILogger<CoinGeckoController> logger)
    {
        _coinGeckoService = coinGeckoService;
        _logger = logger;
    }

    [HttpGet("getBitcoinPrice")] // Endpoint: /api/CoinGecko/getBitcoinPrice (Corrected spelling to Bitcoin)
    public async Task<ActionResult<double>> GetBitcoinPrice()
    {
        _logger.LogInformation("GetBitcoinPrice endpoint called."); // Log endpoint call

        try
        {
            var bitcoinPrice = await _coinGeckoService.GetBitcoinPrice();
            return new ActionResult<double>(bitcoinPrice);
        }
        catch (Exception ex) // Catch any exceptions during price fetching
        {
            _logger.LogError($"Error fetching Bitcoin price: {ex.Message}"); // Log error
            return new ActionResult<double>(0);
        }
    }
}