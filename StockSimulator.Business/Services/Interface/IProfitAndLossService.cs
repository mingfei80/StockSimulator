using StockSimulator.Data.Models;

namespace StockSimulator.Business.Services;

public interface IProfitAndLossService
{
    Task<ProfitAndLoss?> GetByIdAsync(int id);
    Task<ProfitAndLoss> ProcessProfitAndLossAsync(List<int> tradeTransactionIds);
}