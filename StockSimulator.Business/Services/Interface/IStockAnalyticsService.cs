using StockSimulator.Data.Models.Projection;

namespace StockSimulator.Business.Services
{
    public interface IStockAnalyticsService
    {
        Task<List<StockProfitAndLossData>> GetDataBySnapshotStockPriceGroupIdAsync(int snapshotStockPriceGroup);
    }
}