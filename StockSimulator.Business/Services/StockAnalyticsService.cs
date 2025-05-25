using StockSimulator.Data.Models.Projection;
using StockSimulator.Data.Repositories;

namespace StockSimulator.Business.Services;

public class StockAnalyticsService : IStockAnalyticsService
{
    private readonly IStockAnalyticsRepository _stockAnalyticsRepository;
    public StockAnalyticsService(IStockAnalyticsRepository stockAnalyticsRepository)
    {
        _stockAnalyticsRepository = stockAnalyticsRepository;
    }

    public async Task<List<StockProfitAndLossData>> GetDataBySnapshotStockPriceGroupIdAsync(int snapshotStockPriceGroup)
    {
        var result = await _stockAnalyticsRepository.GetStockProfitAndLossAsync();

        return result;
    }
}
