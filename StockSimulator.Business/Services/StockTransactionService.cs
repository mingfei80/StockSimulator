using Microsoft.EntityFrameworkCore;
using StockSimulator.Data.Models;
using StockSimulator.Data.Repositories;

namespace StockSimulator.Business.Services;

public class StockTransactionService: IStockTransactionService
{
    private readonly IStockTransactionRepository _stockTransactionRepository;
    public StockTransactionService(IStockTransactionRepository stockTransactionRepository)
    {
        _stockTransactionRepository = stockTransactionRepository;
    }

    public async Task<List<StockTransaction>> GetAllAsync()
    {
        return await _stockTransactionRepository.GetAllAsync(
            include:
            query => query.Include(u => u.Stock)
                          .Include(v => v.Agent)
                          .Include(w => w.Buyer)
            );
    }
}
