using StockSimulator.Business.Services;
using StockSimulator.Data.Context;
using StockSimulator.Data.Repositories;

namespace StockSimulator.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection StockSimulatorDependecies(this IServiceCollection service)
    {
        service.AddScoped<StockSimulatorDbContext>();

        service.AddScoped<IStockTransactionRepository, StockTransactionRepository>();

        service.AddScoped<IStockTransactionService, StockTransactionService>();

        return service;
    }
}
