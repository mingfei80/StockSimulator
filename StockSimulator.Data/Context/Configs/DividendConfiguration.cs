using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockSimulator.Data.Models;

namespace StockSimulator.Data.Context.Configs;

public class DividendConfiguration : IEntityTypeConfiguration<Dividend>
{
    public void Configure(EntityTypeBuilder<Dividend> builder)
    {
        builder.ToTable("Dividends");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Amount).HasColumnType("decimal(18,2)");
        builder.Property(e => e.ReferenceId).HasMaxLength(100).IsRequired();
        builder.Property(e => e.TradeDate).IsRequired();
        builder.Property(e => e.SettleDate).IsRequired();
        builder.Property(e => e.ImportDate).HasDefaultValueSql("getdate()");

        // Foreign key to Stock
        builder.HasOne(e => e.Stock)
              .WithMany(s => s.Dividends)
              .HasForeignKey(e => e.StockId)
              .OnDelete(DeleteBehavior.Restrict);

        // Foreign key to Agent
        builder.HasOne(e => e.Agent)
              .WithMany(a => a.Dividends)
              .HasForeignKey(e => e.AgentId)
              .OnDelete(DeleteBehavior.Restrict);

        // Foreign key to Buyer
        builder.HasOne(e => e.Buyer)
              .WithMany(b => b.Dividends)
              .HasForeignKey(e => e.BuyerId)
              .OnDelete(DeleteBehavior.Restrict);

        // Foreign key to ProfitAndLoss (nullable)
        builder.HasOne(e => e.ProfitAndLoss)
              .WithMany(p => p.Dividends)
              .HasForeignKey(e => e.ProfitAndLossId)
              .OnDelete(DeleteBehavior.SetNull);
    }
}