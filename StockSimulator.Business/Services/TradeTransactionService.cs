using Microsoft.EntityFrameworkCore;
using StockSimulator.Data.Models;
using StockSimulator.Data.Repositories;

namespace StockSimulator.Business.Services;

public class TradeTransactionService : ITradeTransactionService
{
    private readonly ITradeTransactionRepository _tradeTransactionRepository;
    public TradeTransactionService(ITradeTransactionRepository tradeTransactionRepository)
    {
        _tradeTransactionRepository = tradeTransactionRepository;
    }

    public async Task<List<TradeTransaction>> GetAllAsync()
    {
        return await _tradeTransactionRepository.GetAllAsync(
            include:
            query => query.Include(u => u.Stock)
                          .Include(v => v.Agent)
                          .Include(w => w.Buyer)
            );
    }

    public async Task<List<TradeTransaction>> GetByIdsAsync(List<int> tradetranasctionIds)
    {
        return await _tradeTransactionRepository.GetByIdsAsync(tradetranasctionIds);
    }
    public async Task<List<TradeTransaction>> GetUassignedByStockIdAsync(List<int> stockIds, int buyerId)
    {
        var result = await _tradeTransactionRepository.FindAsync(
            predicate: x => stockIds.Contains(x.StockId) && x.BuyerId == buyerId && x.ProfitAndLossId == null,
            include: query => query.Include(u => u.Stock)
        );

        return result;
    }
}
