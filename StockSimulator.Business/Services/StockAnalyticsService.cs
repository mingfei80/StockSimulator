using StockSimulator.Data.Repositories;

namespace StockSimulator.Business.Services;

public class StockAnalyticsService : IStockAnalyticsService
{
    private readonly IStockAnalyticsRepository _stockAnalyticsRepository;
    public StockAnalyticsService(IStockAnalyticsRepository stockAnalyticsRepository)
    {
        _stockAnalyticsRepository = stockAnalyticsRepository;
    }

    public async Task<StockProfitAndLossSummaryResult> GetDataBySnapshotStockPriceGroupIdAsync(int snapshotStockPriceGroup)
    {
        var stockProfitAndLossData = await _stockAnalyticsRepository.GetStockProfitAndLossAsync(snapshotStockPriceGroup);

        var items = stockProfitAndLossData.ToList();

        return new StockProfitAndLossSummaryResult
        {
            Items = items,
            GrandTotalGrossProfit = Math.Round(items.Sum(x => x.GrossProfit), 2),
            GrandTotalDividends = Math.Round(items.Sum(x => x.TotalDividends), 2),
            GrandTotalFees = Math.Round(items.Sum(x => x.TotalFees), 2)
        };
    }
}
