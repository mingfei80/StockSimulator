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

    public async Task<List<Dividend>> GetByStockIdAsync(int stockId)
    {
        var result = await _dividendRepository.FindOneAsync(
            predicate: x => x.StockId == stockId && x.BuyerId == 1,
            include: query => query.Include(u => u.Stock)
        );

        return result != null ? new List<Dividend> { result } : new List<Dividend>();
    }
}
