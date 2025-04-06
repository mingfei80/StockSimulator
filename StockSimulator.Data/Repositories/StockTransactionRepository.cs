using StockSimulator.Data.Context;
using StockSimulator.Data.Models;
using StockSimulator.Data.Repositories.Generic;

namespace StockSimulator.Data.Repositories;

public class StockTransactionRepository : Repository<StockTransaction>, IStockTransactionRepository
{
    protected readonly StockSimulatorDbContext _context;
    public StockTransactionRepository(StockSimulatorDbContext context) : base(context)
    {
        _context = context;
    }
}
