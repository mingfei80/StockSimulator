using StockSimulator.Data.Models;
using StockSimulator.Data.Repositories.Generic;

namespace StockSimulator.Data.Repositories;

public interface IStockTransactionRepository: IRepository<StockTransaction>
{
}