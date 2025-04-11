using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockSimulator.Data.Models;

namespace StockSimulator.Data.Context.Configs;

public class ProfitAndLossConfiguration : IEntityTypeConfiguration<ProfitAndLoss>
{
    public void Configure(EntityTypeBuilder<ProfitAndLoss> builder)
    {
        builder.ToTable("ProfitAndLoss");

        builder.HasKey(f => f.Id);
        builder.Property(f => f.DaysHolding).HasColumnType("int");
        builder.Property(f => f.GrossProfit).HasColumnType("decimal(18,2)");
        builder.Property(f => f.TotalFees).HasColumnType("decimal(18,2)");
        builder.Property(f => f.TotalDividends).HasColumnType("decimal(18,2)");
    }
}
