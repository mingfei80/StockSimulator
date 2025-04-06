namespace StockSimulator.Data.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class StockTransaction
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Stock")]
    public int StockId { get; set; }
    public required Stock Stock { get; set; }

    [ForeignKey("Agent")]
    public int AgentId { get; set; }
    public required Agent Agent { get; set; }

    [ForeignKey("Buyer")]
    public int BuyerId { get; set; }
    public required Buyer Buyer { get; set; }

    [Required]
    public DateTime TradeDate { get; set; }

    [Required]
    public DateTime SettlementDate { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,4)")]
    public decimal UnitPrice { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,4)")]
    [Range(0.0001, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public decimal Quantity { get; set; }

    [Column(TypeName = "decimal(18,9)")]
    public decimal? ConversionRate { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,4)")]
    public decimal AgentFees { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,4)")]
    public decimal StampDuty { get; set; }

    [Required]
    public bool IsSold { get; set; } = false;
}