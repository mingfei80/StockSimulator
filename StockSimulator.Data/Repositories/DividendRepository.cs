using Microsoft.EntityFrameworkCore;
using StockSimulator.Data.Context;
using StockSimulator.Data.Models;
using StockSimulator.Data.Repositories.Generic;

namespace StockSimulator.Data.Repositories;

public class DividendRepository : Repository<Dividend>, IDividendRepository
{
    protected readonly StockSimulatorDbContext _context;
    public DividendRepository(StockSimulatorDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<List<Dividend>> GetByStockAndDateRangeAsync(int stockId, DateTime minDate, DateTime maxDate)
    {
        var dividends = await _context.Dividends
            .Where(x => x.StockId == stockId && x.TradeDate >= minDate && x.TradeDate <= maxDate)
            .ToListAsync();

        return dividends;
    }
}
