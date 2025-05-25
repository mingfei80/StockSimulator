namespace StockSimulator.Data.Models;
public class SnapshotStockPrice
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public decimal UnitCost { get; set; }
    public decimal LocalUnitCost { get; set; }
    public int SnapshotStockPriceGroupId { get; set; }

    public virtual Stock? Stock { get; set; }
    public virtual SnapshotStockPriceGroup? SnapshotStockPriceGroup { get; set; }
}

