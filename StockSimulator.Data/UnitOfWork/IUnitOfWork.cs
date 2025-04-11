using Microsoft.EntityFrameworkCore.Storage;

namespace StockSimulator.Data.UnitOfWork;

public interface IUnitOfWork
{
    Task<IDbContextTransaction> BeginTransactionAsync();
}