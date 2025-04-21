using StockSimulator.Data.Models;

namespace StockSimulator.Business.Services;

public interface ITradeTransactionService
{
    Task<List<TradeTransaction>> GetAllAsync();
    Task<List<TradeTransaction>> GetStockUnassignedBuySellMatchesAsync(int buyerId);
    Task<List<TradeTransaction>> GetByStockIdWithProfitAndLossIdAsync(int stockId, int buyerId, int? profitAndLossId);
}