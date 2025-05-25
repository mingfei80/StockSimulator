using Microsoft.EntityFrameworkCore;
using StockSimulator.Data.Context.Configs;
using StockSimulator.Data.Models;

namespace StockSimulator.Data.Context;

public class StockSimulatorDbContext : DbContext
{
    public DbSet<TradeTransaction> TradeTransactions { get; set; }
    public DbSet<TradeFee> TradeFees { get; set; }
    public DbSet<Dividend> Dividends { get; set; }
    public DbSet<TradeType> TradeTypes { get; set; }
    public DbSet<ProfitAndLoss> ProfitAndLosses { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Buyer> Buyers { get; set; }
    public DbSet<Agent> Agents { get; set; }
    public DbSet<SnapshotStockPrice> SnapshotStockPrices { get; set; }
    public DbSet<SnapshotStockPriceGroup> SnapshotStockPriceGroups { get; set; }

    public StockSimulatorDbContext(DbContextOptions<StockSimulatorDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AgentConfiguration());
        modelBuilder.ApplyConfiguration(new BuyerConfiguration());
        modelBuilder.ApplyConfiguration(new DividendConfiguration());
        modelBuilder.ApplyConfiguration(new ProfitAndLossConfiguration());
        modelBuilder.ApplyConfiguration(new TradeFeeConfiguration());;
        modelBuilder.ApplyConfiguration(new TradeTransactionConfiguration());
        modelBuilder.ApplyConfiguration(new TradeTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
