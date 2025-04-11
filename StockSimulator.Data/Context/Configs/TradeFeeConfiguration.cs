using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockSimulator.Data.Models;

namespace StockSimulator.Data.Context.Configs;

public class TradeFeeConfiguration : IEntityTypeConfiguration<TradeFee>
{
    public void Configure(EntityTypeBuilder<TradeFee> builder)
    {
        builder.ToTable("TradeFees");

        builder.HasKey(f => f.Id);
        builder.Property(f => f.Amount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(f => f.ReferenceId).HasMaxLength(100).IsRequired();
        builder.Property(e => e.TradeDate).IsRequired();
        builder.Property(e => e.SettleDate).IsRequired();
        builder.Property(f => f.ImportDate).HasDefaultValueSql("getdate()").IsRequired();

        // Foreign key to FeeType
        builder.HasOne(f => f.FeeType)
            .WithMany(t => t.TradeFees)
            .HasForeignKey(f => f.FeeTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Foreign key to Stock
        builder.HasOne(e => e.Stock)
              .WithMany(s => s.TradeFees)
              .HasForeignKey(e => e.StockId)
              .OnDelete(DeleteBehavior.Restrict);

        // Foreign key to Agent
        builder.HasOne(e => e.Agent)
              .WithMany(a => a.TradeFees)
              .HasForeignKey(e => e.AgentId)
              .OnDelete(DeleteBehavior.Restrict);

        // Foreign key to Buyer
        builder.HasOne(e => e.Buyer)
              .WithMany(b => b.TradeFees)
              .HasForeignKey(e => e.BuyerId)
              .OnDelete(DeleteBehavior.Restrict);

        // Foreign key to ProfitAndLoss (nullable)
        builder.HasOne(e => e.ProfitAndLoss)
              .WithMany(p => p.TradeFees)
              .HasForeignKey(e => e.ProfitAndLossId)
              .OnDelete(DeleteBehavior.SetNull);
    }
}
