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

    public async Task<List<TradeFee>> GetByStockIdWithProfitAndLossIdAsync(int stockId, int buyerId, int? profitAndLossId)
    {
        var result = await _tradeFeeRepository.FindAsync(
            predicate: x => x.StockId == stockId && x.BuyerId == buyerId && x.ProfitAndLossId == profitAndLossId,
            include: query => query.Include(u => u.Stock)
        );

        return result;
    }
}
