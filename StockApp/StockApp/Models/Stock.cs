namespace StockApp.Models;

public class Stock
{
    public string? StockSymbol { get; set; }
    public double CurrentPrice { get; set; }
    public double HighestPrice { get; set; }
    public double LowestPrice { get; set; }
    public double OpeningPrice { get; set; }
}