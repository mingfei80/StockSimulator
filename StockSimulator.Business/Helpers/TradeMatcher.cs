using StockSimulator.Data.Models;

namespace StockSimulator.Business.Logic;
public static class TradeMatcher
{
    public static List<TradeTransaction> MatchAndCalculate(List<TradeTransaction> trades, int startProfitId = 1)
    {
        int currentGroupId = startProfitId;

        foreach (var stockGroup in trades
            .Where(t => t.ProfitAndLossId == null)
            .GroupBy(t => t.StockId))
        {
            var buys = new Queue<TradeTransaction>(stockGroup
                .Where(t => !t.IsSold)
                .OrderBy(t => t.TradeDate)
                .ThenBy(t => t.Id));

            var sells = new Queue<TradeTransaction>(stockGroup
                .Where(t => t.IsSold)
                .OrderBy(t => t.TradeDate)
                .ThenBy(t => t.Id));

            while (buys.Any() && sells.Any())
            {
                var buyGroup = new List<TradeTransaction>();
                decimal totalBuyQty = 0;

                // Collect enough buy trades to match next sell
                while (buys.Any() && totalBuyQty < sells.Peek().Quantity)
                {
                    var buy = buys.Dequeue();
                    totalBuyQty += buy.Quantity;
                    buyGroup.Add(buy);
                }

                if (totalBuyQty < sells.Peek().Quantity)
                {
                    // Not enough to match, skip
                    continue;
                }

                var sell = sells.Dequeue();
                decimal
                    remainingSellQty = sell.Quantity;

                // Assign group to sell
                sell.ProfitAndLossId = currentGroupId;

                // Assign group to relevant buys
                foreach (var buy in buyGroup)
                {
                    buy.ProfitAndLossId = currentGroupId;

                    if (buy.Quantity <= remainingSellQty)
                    {
                        remainingSellQty -= buy.Quantity;
                    }
                    else
                    {
                        // If one buy trade has more quantity than needed, push the excess back
                        var excessBuy = new TradeTransaction
                        {
                            Id = buy.Id,
                            StockId = buy.StockId,
                            Quantity = buy.Quantity - remainingSellQty,
                            IsSold = false,
                            TradeDate = buy.TradeDate
                        };
                        trades.Add(excessBuy); // Add excess back to trades
                        remainingSellQty = 0;
                        break;
                    }
                }

                currentGroupId++;
            }
        }

        return trades;
    }
}
