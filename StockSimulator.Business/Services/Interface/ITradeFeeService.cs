using StockSimulator.Data.Models;

namespace StockSimulator.Business.Services;

public interface ITradeFeeService
{
    Task<List<TradeFee>> GetByStockIdAsync(int stockId);
}