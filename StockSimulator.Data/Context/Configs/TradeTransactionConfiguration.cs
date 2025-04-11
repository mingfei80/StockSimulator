using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockSimulator.Data.Models;

namespace StockSimulator.Data.Context.Configs;

public class TradeTransactionConfiguration : IEntityTypeConfiguration<TradeTransaction>
{
    public void Configure(EntityTypeBuilder<TradeTransaction> builder)
    {
        builder.ToTable("TradeTransactions");

        builder.HasKey(t => t.Id);
        builder.Property(e => e.TradeDate).IsRequired();
        builder.Property(e => e.SettleDate).IsRequired();
        builder.Property(e => e.UnitCost).HasColumnType("decimal(18,4)").IsRequired();
        builder.Property(e => e.Quantity).HasColumnType("decimal(18,4)").IsRequired();
        builder.Property(e => e.TransactionAmount).HasColumnType("decimal(18,2)").IsRequired();        
        builder.Property(e => e.ConversionRate).HasColumnType("decimal(18,4)");
        builder.Property(e => e.ImportId).IsRequired();
        builder.Property(e => e.UnitCostForeign).HasColumnType("decimal(18,2)");
        builder.Property(e => e.IsSold).HasColumnType("bit").IsRequired();
        builder.Property(e => e.ReferenceId).HasMaxLength(100).IsRequired();
        builder.Property(e => e.ImportDate).HasDefaultValueSql("getdate()").IsRequired();

        // Foreign key to Stock
        builder.HasOne(e => e.Stock)
              .WithMany(s => s.TradeTransactions)
              .HasForeignKey(e => e.StockId)
              .OnDelete(DeleteBehavior.Restrict);

        // Foreign key to Agent
        builder.HasOne(e => e.Agent)
              .WithMany(a => a.TradeTransactions)
              .HasForeignKey(e => e.AgentId)
              .OnDelete(DeleteBehavior.Restrict);

        // Foreign key to Buyer
        builder.HasOne(e => e.Buyer)
              .WithMany(b => b.TradeTransactions)
              .HasForeignKey(e => e.BuyerId)
              .OnDelete(DeleteBehavior.Restrict);

        // Foreign key to ProfitAndLoss (nullable)
        builder.HasOne(e => e.ProfitAndLoss)
              .WithMany(p => p.TradeTransactions)
              .HasForeignKey(e => e.ProfitAndLossId)
              .OnDelete(DeleteBehavior.SetNull);
    }
}