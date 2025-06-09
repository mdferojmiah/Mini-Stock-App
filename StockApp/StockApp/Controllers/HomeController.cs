using Microsoft.AspNetCore.Mvc;
using StockApp.Models;
using StockApp.ServiceContracts;
using StockApp.Services;

namespace StockApp.Controllers;

public class HomeController(IFinnhubService finnhubService) : Controller
{
    private readonly IFinnhubService _finnhubService = finnhubService;
    [Route("/{symbol}")]
    [Route("/")]
    public async Task<IActionResult> Index(string? symbol)
    {
        symbol ??= "MSFT";
        Dictionary<string, object>? responseDictionary = await _finnhubService.GetStockPrice(symbol);
        Stock stock = new Stock()
        {
            StockSymbol = symbol,
            CurrentPrice = Convert.ToDouble(responseDictionary!["c"].ToString()),
            HighestPrice = Convert.ToDouble(responseDictionary!["h"].ToString()),
            LowestPrice = Convert.ToDouble(responseDictionary!["l"].ToString()),
            OpeningPrice = Convert.ToDouble(responseDictionary!["o"].ToString())
        };
        return View(stock);
    }
}