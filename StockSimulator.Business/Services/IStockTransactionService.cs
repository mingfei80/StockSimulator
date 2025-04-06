using StockSimulator.Data.Models;

namespace StockSimulator.Business.Services;

public interface IStockTransactionService
{
    Task<List<StockTransaction>> GetAllAsync();
}