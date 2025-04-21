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
    public async Task<List<TradeTransaction>> GetStockUnassignedBuySellMatchesAsync(int buyerId)
    {
        var result = await _tradeTransactionRepository.FindAsync(
            predicate: x => x.BuyerId == buyerId && x.ProfitAndLossId == null,
            include: query => query.Include(u => u.Stock)
        );

        return result;
    }

    public async Task<List<TradeTransaction>> GetByStockIdWithProfitAndLossIdAsync(int stockId, int buyerId, int? profitAndLossId)
    {
        var result = await _tradeTransactionRepository.FindAsync(
            predicate: x => x.StockId == stockId && x.BuyerId == buyerId && x.ProfitAndLossId == profitAndLossId,
            include: query => query.Include(u => u.Stock)
        );

        return result;
    }
}
