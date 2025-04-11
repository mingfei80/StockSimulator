using StockSimulator.Data.Context;
using StockSimulator.Data.Models;
using StockSimulator.Data.Repositories.Generic;

namespace StockSimulator.Data.Repositories;

public class ProfitAndLossRepository : Repository<ProfitAndLoss>, IProfitAndLossRepository
{
    protected readonly StockSimulatorDbContext _context;
    public ProfitAndLossRepository(StockSimulatorDbContext context) : base(context)
    {
        _context = context;
    }

}
