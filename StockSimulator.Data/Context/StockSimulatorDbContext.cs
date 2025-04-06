using Microsoft.EntityFrameworkCore;
using StockSimulator.Data.Models;

namespace StockSimulator.Data.Context;

public class StockSimulatorDbContext : DbContext
{
    public DbSet<StockTransaction> StockTransactions { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Buyer> Buyers { get; set; }
    public DbSet<Agent> Agents { get; set; }

    public StockSimulatorDbContext(DbContextOptions<StockSimulatorDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StockTransaction>()
            .ToTable("StockTransactions")
            .ToTable(x => x.HasCheckConstraint("CK_StockTransactions_Quantity", "[Quantity] > 0"));

        modelBuilder.Entity<StockTransaction>(entity =>
        {
            entity.HasOne(st => st.Stock)
                .WithMany(s => s.StockTransactions)
                .HasForeignKey(st => st.StockId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(st => st.Agent)
                .WithMany(a => a.StockTransactions)
                .HasForeignKey(st => st.AgentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(st => st.Buyer)
                .WithMany(b => b.StockTransactions)
                .HasForeignKey(st => st.BuyerId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(st => st.IsSold)
                .HasDefaultValueSql("0");
                //.HasDefaultValue(false); // Equivalent to DEFAULT ((0))

        });


        modelBuilder.Entity<Stock>()
            .HasIndex(s => s.StockCode)
            .IsUnique();

    }
}
