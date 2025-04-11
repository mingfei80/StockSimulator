using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockSimulator.Data.Models;

namespace StockSimulator.Data.Context.Configs;

public class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
{
    public void Configure(EntityTypeBuilder<Buyer> builder)
    {
        builder.ToTable("Buyers");

        builder.HasKey(f => f.Id);
        builder.Property(f => f.Name).HasMaxLength(255);
    }
}
