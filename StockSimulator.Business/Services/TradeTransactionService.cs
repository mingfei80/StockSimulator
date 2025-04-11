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

    public async Task<List<TradeTransaction>> GetByIdsAsync(List<int> stockIds)
    {
        return await _tradeTransactionRepository.GetByIdsAsync(stockIds);
    }
    public async Task<List<TradeTransaction>> GetByStockIdAsync(int stockId)
    {
        var result = await _tradeTransactionRepository.FindOneAsync(
            predicate: x => x.StockId == stockId && x.BuyerId == 1,
            include: query => query.Include(u => u.Stock)
        );

        return result != null ? new List<TradeTransaction> { result } : new List<TradeTransaction>();
    }
}
