using StockSimulator.Data.Models;

namespace StockSimulator.Business.Services;

public interface ITradeTransactionService
{
    Task<List<TradeTransaction>> GetAllAsync();
    Task<List<TradeTransaction>> GetByIdsAsync(List<int> tradetranasctionIds);
    Task<List<TradeTransaction>> GetUassignedByStockIdAsync(List<int> stockIds, int buyerId);
}