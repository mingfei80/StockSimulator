using StockSimulator.Data.Models;

namespace StockSimulator.Business.Services;

public interface ITradeTransactionService
{
    Task<List<TradeTransaction>> GetAllAsync();
    Task<List<TradeTransaction>> GetByIdsAsync(List<int> stockIds);
    Task<List<TradeTransaction>> GetByStockIdAsync(int stockId);
}