using StockSimulator.Data.Models;

namespace StockSimulator.Business.Services;

public interface IDividendService
{
    Task<List<Dividend>> GetByStockIdAsync(int stockId);
}