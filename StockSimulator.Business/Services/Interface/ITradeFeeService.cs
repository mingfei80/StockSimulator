using StockSimulator.Data.Models;

namespace StockSimulator.Business.Services;

public interface ITradeFeeService
{
    Task<List<TradeFee>> GetByStockIdWithProfitAndLossIdAsync(int stockId, int buyerId, int? profitAndLossId);
}