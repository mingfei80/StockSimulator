using Microsoft.EntityFrameworkCore.Storage;
using StockSimulator.Data.Context;

namespace StockSimulator.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly StockSimulatorDbContext _context;

    public UnitOfWork(StockSimulatorDbContext context)
    {
        _context = context;
    }

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return _context.Database.BeginTransactionAsync();
    }
}