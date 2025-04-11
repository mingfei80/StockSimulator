using Microsoft.EntityFrameworkCore;
using StockSimulator.Data.Context;
using StockSimulator.Data.Models;
using StockSimulator.Data.Repositories.Generic;

namespace StockSimulator.Data.Repositories;

public class TradeTransactionRepository : Repository<TradeTransaction>, ITradeTransactionRepository
{
    protected readonly StockSimulatorDbContext _context;
    public TradeTransactionRepository(StockSimulatorDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<TradeTransaction>> GetByIdsAsync(List<int> ids)
    {
        var trades = await _context.TradeTransactions
            .Where(x => ids.Contains(x.Id))
            .Include(u => u.Stock)
            .Include(v => v.Agent)
            .Include(w => w.Buyer)
            .ToListAsync();

        return trades;
    }
}
