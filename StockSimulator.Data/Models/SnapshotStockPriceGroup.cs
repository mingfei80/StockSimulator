namespace StockSimulator.Data.Models;
public class SnapshotStockPriceGroup
{
    public int Id { get; set; }
    public DateOnly SnapshotDate { get; set; }

    public virtual ICollection<SnapshotStockPrice>? SnapshotStockPrices { get; set; }
}

