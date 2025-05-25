using StockSimulator.Business.Services;
using StockSimulator.Data.Context;
using StockSimulator.Data.Repositories;
using StockSimulator.Data.UnitOfWork;

namespace StockSimulator.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection StockSimulatorDependecies(this IServiceCollection service)
    {
        service.AddScoped<StockSimulatorDbContext>();

        service.AddScoped<IUnitOfWork, UnitOfWork>();

        service.AddScoped<IProfitAndLossRepository, ProfitAndLossRepository>();
        service.AddScoped<ITradeTransactionRepository, TradeTransactionRepository>();
        service.AddScoped<IDividendRepository, DividendRepository>();
        service.AddScoped<ITradeFeeRepository, TradeFeeRepository>();
        service.AddScoped<IStockAnalyticsRepository, StockAnalyticsRepository>();

        service.AddScoped<IProfitAndLossService, ProfitAndLossService>();
        service.AddScoped<ITradeTransactionService, TradeTransactionService>();
        service.AddScoped<IDividendService, DividendService>();
        service.AddScoped<ITradeFeeService, TradeFeeService>();
        service.AddScoped<IStockAnalyticsService, StockAnalyticsService>();

        return service;
    }
}
