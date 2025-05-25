using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockSimulator.Data.Models;

namespace StockSimulator.Data.Context.Configs;

public class SnapshotStockPriceGroupConfiguration : IEntityTypeConfiguration<SnapshotStockPriceGroup>
{
    public void Configure(EntityTypeBuilder<SnapshotStockPriceGroup> builder)
    {
        builder.ToTable("SnapshotStockPriceGroups");

        builder.HasKey(t => t.Id);
        builder.Property(e => e.SnapshotDate).HasColumnType("date").IsRequired();

    }
}