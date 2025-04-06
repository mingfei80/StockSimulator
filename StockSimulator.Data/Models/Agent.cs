namespace StockSimulator.Data.Models;

using System.ComponentModel.DataAnnotations;
public class Agent
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public required string AgentName { get; set; }

    public ICollection<StockTransaction>? StockTransactions { get; set; }
}

