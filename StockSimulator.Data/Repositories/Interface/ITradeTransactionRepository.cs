using StockSimulator.Data.Models;
using StockSimulator.Data.Repositories.Generic;

namespace StockSimulator.Data.Repositories;

public interface ITradeTransactionRepository: IRepository<TradeTransaction>
{
    Task<List<TradeTransaction>> GetByIdsAsync(List<int> ids);
}