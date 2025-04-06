namespace StockSimulator.Data.Models;

using System.ComponentModel.DataAnnotations;

public class Buyer
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string BuyerName { get; set; }

    public ICollection<StockTransaction>? StockTransactions { get; set; }
}