using Microsoft.EntityFrameworkCore;
using StockSimulator.Data.Models;
using StockSimulator.Data.Repositories;

namespace StockSimulator.Business.Services;

public class DividendService : IDividendService
{
    private readonly IDividendRepository _dividendRepository;
    public DividendService(IDividendRepository dividendRepository)
    {
        _dividendRepository = dividendRepository;
    }

    public async Task<List<Dividend>> GetByStockIdWithProfitAndLossIdAsync(int stockId, int buyerId, int? profitAndLossId)
    {
        var result = await _dividendRepository.FindAsync(
            predicate: x => x.StockId == stockId && x.BuyerId == 1 && x.ProfitAndLossId == profitAndLossId,
            include: query => query.Include(u => u.Stock)
        );

        return result;
    }
}
