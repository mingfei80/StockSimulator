using Microsoft.EntityFrameworkCore;
using StockSimulator.Data.Models;
using StockSimulator.Data.Repositories;

namespace StockSimulator.Business.Services;

public class TradeFeeService : ITradeFeeService
{
    private readonly ITradeFeeRepository _tradeFeeRepository;
    public TradeFeeService(ITradeFeeRepository tradeFeeRepository)
    {
        _tradeFeeRepository = tradeFeeRepository;
    }

    public async Task<List<TradeFee>> GetByStockIdAsync(int stockId)
    {
        var result = await _tradeFeeRepository.FindOneAsync(
            predicate: x => x.StockId == stockId && x.BuyerId == 1,
            include: query => query.Include(u => u.Stock)
        );

        return result != null ? new List<TradeFee> { result } : new List<TradeFee>();
    }
}
