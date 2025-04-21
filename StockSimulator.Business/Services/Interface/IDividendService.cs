using StockSimulator.Data.Models;

namespace StockSimulator.Business.Services;

public interface IDividendService
{
    Task<List<Dividend>> GetByStockIdWithProfitAndLossIdAsync(int stockId, int buyerId, int? profitAndLossId);
}