using Microsoft.EntityFrameworkCore;
using StockSimulator.Data.Context;
using StockSimulator.Data.Models;
using StockSimulator.Data.Repositories.Generic;

namespace StockSimulator.Data.Repositories;

public class TradeFeeRepository : Repository<TradeFee>, ITradeFeeRepository
{
    protected readonly StockSimulatorDbContext _context;
    public TradeFeeRepository(StockSimulatorDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<List<TradeFee>> GetByStockAndDateRangeAsync(int stockId, DateTime minDate, DateTime maxDate)
    {
        var tradefees = await _context.TradeFees
            .Where(x => x.StockId == stockId && x.TradeDate >= minDate && x.TradeDate <= maxDate)
            .ToListAsync();

        return tradefees;
    }
}
