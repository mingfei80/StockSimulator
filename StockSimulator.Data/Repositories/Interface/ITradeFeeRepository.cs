using StockSimulator.Data.Models;
using StockSimulator.Data.Repositories.Generic;

namespace StockSimulator.Data.Repositories;

public interface ITradeFeeRepository : IRepository<TradeFee>
{
    Task<List<TradeFee>> GetByStockAndDateRangeAsync(int stockId, DateTime minDate, DateTime maxDate);
}