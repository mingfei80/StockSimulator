using StockSimulator.Data.Models.Projection;

namespace StockSimulator.Data.Repositories;

public interface IStockAnalyticsRepository
{
    Task<List<StockProfitAndLossData>> GetStockProfitAndLossAsync();
}