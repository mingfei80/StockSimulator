using StockSimulator.Data.Models.Projection;

namespace StockSimulator.Business.Services
{
    public interface IStockAnalyticsService
    {
        Task<StockProfitAndLossSummaryResult> GetDataBySnapshotStockPriceGroupIdAsync(int snapshotStockPriceGroup);
    }
}