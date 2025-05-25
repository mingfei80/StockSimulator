using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockSimulator.Data.Models;

namespace StockSimulator.Data.Context.Configs;

public class SnapshotStockPriceConfiguration : IEntityTypeConfiguration<SnapshotStockPrice>
{
    public void Configure(EntityTypeBuilder<SnapshotStockPrice> builder)
    {
        builder.ToTable("SnapshotStockPrices");

        builder.HasKey(t => t.Id);
        builder.Property(e => e.StockId).IsRequired();
        builder.Property(e => e.UnitCost).HasColumnType("decimal(18,4)").IsRequired();
        builder.Property(e => e.SnapshotStockPriceGroup).IsRequired();

        // Foreign key to Stock
        builder.HasOne(e => e.Stock)
              .WithMany(s => s.SnapshotStockPrices)
              .HasForeignKey(e => e.StockId)
              .OnDelete(DeleteBehavior.Restrict);

        // Foreign key to SnapshotStockPriceGroup (nullable)
        builder.HasOne(e => e.SnapshotStockPriceGroup)
              .WithMany(p => p.SnapshotStockPrices)
              .HasForeignKey(e => e.SnapshotStockPriceGroupId)
              .OnDelete(DeleteBehavior.Restrict);
    }
}