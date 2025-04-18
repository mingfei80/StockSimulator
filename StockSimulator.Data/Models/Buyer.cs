﻿namespace StockSimulator.Data.Models;
public class Buyer
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public virtual ICollection<TradeTransaction>? TradeTransactions { get; set; }
    public virtual ICollection<Dividend>? Dividends { get; set; }
    public virtual ICollection<TradeFee>? TradeFees { get; set; }
}