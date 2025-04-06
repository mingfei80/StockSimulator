namespace StockSimulator.Data.Models;

using System.ComponentModel.DataAnnotations;

public class Stock
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string StockCode { get; set; }

    [Required]
    [StringLength(255)]
    public required string StockName { get; set; }

    public ICollection<StockTransaction>? StockTransactions { get; set; }
}