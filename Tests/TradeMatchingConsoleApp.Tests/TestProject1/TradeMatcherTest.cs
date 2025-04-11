using TradeMatchingConsoleApp.Logic;
using TradeMatchingConsoleApp.Models;

namespace TradeMatcherTest;

public class TradeMatcherTests
{
    [TestFixture]
    public class TradeProcessorTests
    {
        [Test]
        public void MatchTrades_AssignsCorrectProfitAndLossGroups()
        {
            var trades = new List<TradeTransaction>
            {
                new TradeTransaction { Id = 1, StockId = 1, Quantity = 10, IsSold = false },
                new TradeTransaction { Id = 2, StockId = 1, Quantity = 10, IsSold = true },

                new TradeTransaction { Id = 3, StockId = 2, Quantity = 5, IsSold = false },
                new TradeTransaction { Id = 4, StockId = 2, Quantity = 5, IsSold = true },
                new TradeTransaction { Id = 5, StockId = 2, Quantity = 5, IsSold = false },

                new TradeTransaction { Id = 6, StockId = 3, Quantity = 4, IsSold = false },
                new TradeTransaction { Id = 7, StockId = 3, Quantity = 4, IsSold = false },
                new TradeTransaction { Id = 8, StockId = 3, Quantity = 3, IsSold = false },
                new TradeTransaction { Id = 9, StockId = 3, Quantity = 10, IsSold = true },

                new TradeTransaction { Id = 10, StockId = 4, Quantity = 2, IsSold = false },
                new TradeTransaction { Id = 11, StockId = 4, Quantity = 2, IsSold = true },
                new TradeTransaction { Id = 12, StockId = 4, Quantity = 2, IsSold = false },
                new TradeTransaction { Id = 13, StockId = 4, Quantity = 2, IsSold = true },

                new TradeTransaction { Id = 14, StockId = 5, Quantity = 7, IsSold = false },

                new TradeTransaction { Id = 15, StockId = 6, Quantity = 70, IsSold = false },
                new TradeTransaction { Id = 16, StockId = 6, Quantity = 7, IsSold = true },
                new TradeTransaction { Id = 17, StockId = 6, Quantity = 7, IsSold = true }
            };

            // Act
            int startProfitId = 1;
            var updatesTrades = TradeMatcher.MatchAndCalculate(trades, startProfitId);

            // Assert

            Assert.That(updatesTrades.First(t => t.Id >= 1 && t.Id <= 2).ProfitAndLossId, Is.EqualTo(1));

            Assert.That(updatesTrades.First(t => t.Id >= 3 && t.Id <= 4).ProfitAndLossId, Is.EqualTo(2));

            Assert.That(updatesTrades.First(t => t.Id == 5).ProfitAndLossId, Is.Null);

            Assert.That(updatesTrades.First(t => t.Id >= 6 && t.Id <= 9).ProfitAndLossId, Is.EqualTo(3));

            Assert.That(updatesTrades.First(t => t.Id >= 10 && t.Id <= 11).ProfitAndLossId, Is.EqualTo(4));

            Assert.That(updatesTrades.First(t => t.Id >= 12 && t.Id <= 13).ProfitAndLossId, Is.EqualTo(5));
            Assert.That(updatesTrades.First(t => t.Id > 13).ProfitAndLossId, Is.Null);

            //var group1 = pnlGroups.FirstOrDefault(p => p.Id == 1);
            //Assert.That(group1?.TradeIds[0], Is.EqualTo(1));
            //Assert.That(group1?.TradeIds[1], Is.EqualTo(2));
            //Assert.That(group1?.TradeIds.Count, Is.EqualTo(2));

            //Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 2)?.TradeIds[0], Is.EqualTo(3));
            //Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 2)?.TradeIds[1], Is.EqualTo(4));
            //Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 2)?.TradeIds.Count, Is.EqualTo(2));

            //Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 3)?.TradeIds[0], Is.EqualTo(6));
            //Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 3)?.TradeIds[1], Is.EqualTo(7));
            //Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 3)?.TradeIds[2], Is.EqualTo(8));
            //Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 3)?.TradeIds[3], Is.EqualTo(9));
            //Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 3)?.TradeIds.Count, Is.EqualTo(4));

            //Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 4)?.TradeIds[0], Is.EqualTo(10));
            //Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 4)?.TradeIds[1], Is.EqualTo(11));
            //Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 4)?.TradeIds.Count, Is.EqualTo(2));

            //Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 5)?.TradeIds[0], Is.EqualTo(12));
            //Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 5)?.TradeIds[1], Is.EqualTo(13));
            //Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 5)?.TradeIds.Count, Is.EqualTo(2));


            //foreach (var id in unmatchedIds)
            //{
            //    Assert.That(trades.First(t => t.Id == id).ProfitAndLossId, Is.Null, $"Trade {id} should not be assigned a ProfitAndLossId");
            //}
            //Assert.Multiple(() =>
            //{
            //    Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 1)?.TradeIds, Is.EquivalentTo(new[] { 1, 2 }));
            //    Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 2)?.TradeIds, Is.EquivalentTo(new[] { 3, 4 }));
            //    Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 3)?.TradeIds, Is.EquivalentTo(new[] { 6, 7, 8, 9 }));
            //    Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 4)?.TradeIds, Is.EquivalentTo(new[] { 10, 11 }));
            //    Assert.That(pnlGroups.FirstOrDefault(p => p.Id == 5)?.TradeIds, Is.EquivalentTo(new[] { 12, 13 }));

            //    var unmatchedIds = new[] { 5, 14, 15, 16, 17 };
            //    foreach (var id in unmatchedIds)
            //    {
            //        Assert.That(trades.First(t => t.Id == id).ProfitAndLossId, Is.Null, $"Trade {id} should not be assigned a ProfitAndLossId");
            //    }
            //});
        }
    }
    //[Test]
    //public void MatchTrades_WithSampleData_MatchesCorrectly()
    //{

    //    //var trades = new List<TradeTransaction>
    //    //{
    //    //    new() { Id = 1, StockId = 1, IsSold = false, Quantity = 10, TransactionAmount = 100, TradeDate = new DateTime(2024, 01, 01) },
    //    //    new() { Id = 2, StockId = 1, IsSold = true, Quantity = 10, TransactionAmount = 120, TradeDate = new DateTime(2024, 02, 01) },
    //    //    new() { Id = 3, StockId = 2, IsSold = false, Quantity = 5, TransactionAmount = 50, TradeDate = new DateTime(2024, 01, 01) },
    //    //    new() { Id = 4, StockId = 2, IsSold = true, Quantity = 5, TransactionAmount = 60, TradeDate = new DateTime(2024, 02, 01) },
    //    //    new() { Id = 5, StockId = 2, IsSold = false, Quantity = 5, TransactionAmount = 55, TradeDate = new DateTime(2024, 03, 01) }
    //    //};

    //    var trades = new List<TradeTransaction>
    //        {
    //            new() { Id = 1, StockId = 1, Quantity = 10, IsSold = false },
    //            new() { Id = 2, StockId = 1, Quantity = 10, IsSold = true },

    //            new() { Id = 3, StockId = 2, Quantity = 5, IsSold = false },
    //            new() { Id = 4, StockId = 2, Quantity = 5, IsSold = true },
    //            new() { Id = 5, StockId = 2, Quantity = 5, IsSold = false },

    //            new() { Id = 6, StockId = 3, Quantity = 4, IsSold = false },
    //            new() { Id = 7, StockId = 3, Quantity = 4, IsSold = false },
    //            new() { Id = 8, StockId = 3, Quantity = 3, IsSold = false },
    //            new() { Id = 9, StockId = 3, Quantity = 10, IsSold = true },

    //            new() { Id = 10, StockId = 4, Quantity = 2, IsSold = false },
    //            new() { Id = 11, StockId = 4, Quantity = 2, IsSold = true },
    //            new() { Id = 12, StockId = 4, Quantity = 2, IsSold = false },
    //            new() { Id = 13, StockId = 4, Quantity = 2, IsSold = true },

    //            new() { Id = 14, StockId = 5, Quantity = 7, IsSold = false },

    //            new() { Id = 15, StockId = 6, Quantity = 70, IsSold = false },
    //            new() { Id = 16, StockId = 6, Quantity = 7, IsSold = true },
    //            new() { Id = 17, StockId = 6, Quantity = 7, IsSold = true }
    //        };

    //    // Act

    //    var results = TradeMatcher.MatchAndCalculate(trades);

    //    // Assert

    //    Assert.That(results[0].TradeIds[0], Is.EqualTo(1));
    //    Assert.That(results[0].TradeIds[1], Is.EqualTo(2));
    //    Assert.That(results[0].TradeIds.Count, Is.EqualTo(2));


    //    Assert.That(results[1].TradeIds[0], Is.EqualTo(3));
    //    Assert.That(results[1].TradeIds[1], Is.EqualTo(4));
    //    Assert.That(results[1].TradeIds.Count, Is.EqualTo(2));


    //    Assert.That(results.Count, Is.EqualTo(2));
    //    //Assert.That(results[0].TradeIds.Count, Is.EqualTo(2));
    //    //Assert.That(results[0].TradeIds[1], Is.EqualTo(2));
    //    //Assert.That(results[0].TradeIds[0], Is.EqualTo(1));
    //    //Assert.That(results[0].TradeIds[0], Is.EqualTo(1));
    //    //Assert.That(results[0].TradeIds[0], Is.EqualTo(1));
    //    //var a = results.Where(a => a.TradeIds.Equals(4));
    //    //var f = (ProfitAndLoss)lookup[4];
    //    //List<int> group1 = new List<int>() { 1, 2};
    //    //Assert.IsTrue(lookup[0].TradeIds.Contains(1));
    //    //Assert.IsTrue(lookup[0].TradeIds.Contains(2));
    //    //Assert.IsTrue(lookup[1].TradeIds.Contains(3));

    //    //Assert.That(lookup[1].ProfitAndLossId, Is.EqualTo(1));
    //    //Assert.That(lookup[2].ProfitAndLossId, Is.EqualTo(1));

    //    //Assert.That(lookup[3].ProfitAndLossId, Is.EqualTo(2));
    //    //Assert.That(lookup[4].ProfitAndLossId, Is.EqualTo(2));
    //    //Assert.That(lookup[5].ProfitAndLossId, Is.Null);

    //    //Assert.That(lookup[6].ProfitAndLossId, Is.EqualTo(3));
    //    //Assert.That(lookup[7].ProfitAndLossId, Is.EqualTo(3));
    //    //Assert.That(lookup[8].ProfitAndLossId, Is.EqualTo(3));
    //    //Assert.That(lookup[9].ProfitAndLossId, Is.EqualTo(3));

    //    //Assert.That(lookup[10].ProfitAndLossId, Is.EqualTo(4));
    //    //Assert.That(lookup[11].ProfitAndLossId, Is.EqualTo(4));
    //    //Assert.That(lookup[12].ProfitAndLossId, Is.EqualTo(5));
    //    //Assert.That(lookup[13].ProfitAndLossId, Is.EqualTo(5));

    //    //Assert.That(lookup[14].ProfitAndLossId, Is.Null);

    //    //Assert.That(lookup[15].ProfitAndLossId, Is.Null);
    //    //Assert.That(lookup[16].ProfitAndLossId, Is.Null);
    //    //Assert.That(lookup[17].ProfitAndLossId, Is.Null);
    //}
}