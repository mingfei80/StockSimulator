using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockSimulator.Data.Models;

namespace StockSimulator.Data.Context.Configs;

public class TradeTypeConfiguration : IEntityTypeConfiguration<TradeType>
{
    public void Configure(EntityTypeBuilder<TradeType> builder)
    {
        builder.ToTable("TradeTypes");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
    }
}