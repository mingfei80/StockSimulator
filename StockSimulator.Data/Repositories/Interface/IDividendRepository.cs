using StockSimulator.Data.Models;
using StockSimulator.Data.Repositories.Generic;

namespace StockSimulator.Data.Repositories;

public interface IDividendRepository : IRepository<Dividend>
{
    Task<List<Dividend>> GetByStockAndDateRangeAsync(int stockId, DateTime minDate, DateTime maxDate);
}