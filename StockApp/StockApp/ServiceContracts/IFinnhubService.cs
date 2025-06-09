namespace StockApp.ServiceContracts;

public interface IFinnhubService
{
    public Task<Dictionary<string, object>?> GetStockPrice(string symbol);
}