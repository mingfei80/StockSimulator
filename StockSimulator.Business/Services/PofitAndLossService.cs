using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StockSimulator.Business.Dtos;
using StockSimulator.Business.Logic;
using StockSimulator.Data.Context;
using StockSimulator.Data.Models;
using StockSimulator.Data.Repositories;
using StockSimulator.Data.UnitOfWork;

namespace StockSimulator.Business.Services;

public class ProfitAndLossService: IProfitAndLossService
{
    private readonly ILogger<ProfitAndLossService> _logger;
    private readonly ITradeTransactionRepository _tradeTransactionRepository;
    private readonly IDividendRepository _dividendRepository;
    private readonly ITradeFeeRepository _tradeFeeRepository;
    private readonly IProfitAndLossRepository _profitAndLossRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProfitAndLossService(
        ILogger<ProfitAndLossService> logger,
        ITradeTransactionRepository tradeTransactionRepository,
        IDividendRepository dividendRepository,
        ITradeFeeRepository tradeFeeRepository,
        IProfitAndLossRepository profitAndLossRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _tradeTransactionRepository = tradeTransactionRepository;
        _dividendRepository = dividendRepository;
        _tradeFeeRepository = tradeFeeRepository;
        _profitAndLossRepository = profitAndLossRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProfitAndLoss> ProcessProfitAndLossAsync(List<int> tradeTransactionIds, List<int> dividendIds, List<int> tradeFeeIds)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            var trades = await _tradeTransactionRepository.GetByIdsAsync(tradeTransactionIds);
            if (!trades.Any())
                throw new InvalidOperationException("No trades found for the given IDs.");

            if (trades.GroupBy(x => x.BuyerId).Count() > 1)
                throw new InvalidOperationException("All trades must belong to the same buyer.");

            if (trades.GroupBy(x => x.StockId).Count() > 1)
                throw new InvalidOperationException("All trades must belong to the same stock.");

            if (trades.Where(x => x.IsSold == true).Sum(x => x.Quantity) != trades.Where(x => x.IsSold == false).Sum(x => x.Quantity))
                throw new InvalidOperationException("The total quantity of sold trades must equal the total quantity of bought trades.");

            var stockId = trades.First().StockId;

            // Get Dividends and Trade Fees for the stock within the date range
            var dividends = await _dividendRepository.FindAsync(x => dividendIds.Contains(x.Id), false);
            //await _dividendRepository.GetByIdsAsync(dividendIds);
            var tradeFees = await _tradeFeeRepository.FindAsync(x => tradeFeeIds.Contains(x.Id), false);

            // Calculate Profit and Loss
            ProfitAndLossCalculationResult plDto = ProfitAndLossCalculator.Calculate(trades, dividends, tradeFees);
            var pnl = new ProfitAndLoss
            {
                GrossProfit = plDto.GrossProfit,
                TotalDividends = plDto.TotalDividends,
                TotalFees = plDto.TotalFees,
                DaysHolding = plDto.DaysHolding,
                TradeTransactions = trades.ToList(),
                Dividends = dividends.ToList(),
                TradeFees = tradeFees.ToList()
            };

            await _profitAndLossRepository.AddAsync(pnl);

            // Link ProfitAndLossId to each Trade, Devidend & Fees
            foreach (var trade in trades)
                trade.ProfitAndLossId = pnl.Id;

            foreach (var dividend in dividends)
                dividend.ProfitAndLossId = pnl.Id;

            foreach (var fee in tradeFees)
                fee.ProfitAndLossId = pnl.Id;

            // Save all
            await _tradeTransactionRepository.UpdateRangeAsync(trades);
            await _dividendRepository.UpdateRangeAsync(dividends);
            await _tradeFeeRepository.UpdateRangeAsync(tradeFees);

            await transaction.CommitAsync();

            return pnl;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Error occurred in {Method} with TradeTransactionIds: {Ids}",
                nameof(ProcessProfitAndLossAsync), tradeTransactionIds);
            throw;
        }
    }

    public async Task<ProfitAndLoss> ProcessProfitAndLossAsync(List<int> tradeTransactionIds)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            var trades = await _tradeTransactionRepository.GetByIdsAsync(tradeTransactionIds);
            if (!trades.Any())
                throw new InvalidOperationException("No trades found for the given IDs.");

            if (trades.GroupBy(x => x.BuyerId).Count() > 1)
                throw new InvalidOperationException("All trades must belong to the same buyer.");

            if (trades.GroupBy(x => x.StockId).Count() > 1)
                throw new InvalidOperationException("All trades must belong to the same stock.");

            if (trades.Where(x => x.IsSold == true).Sum(x => x.Quantity) != trades.Where(x => x.IsSold == false).Sum(x => x.Quantity))
                throw new InvalidOperationException("The total quantity of sold trades must equal the total quantity of bought trades.");

            var stockId = trades.First().StockId;

            int backDateDividend5DaysJustInCase = 5;
            int addDateTradeFee10DaysJustInCase = 10;

            var minDividendDate = trades.Min(t => t.TradeDate);
            var maxDividendDate = trades.Max(t => t.TradeDate.AddDays(addDateTradeFee10DaysJustInCase));

            var minTradeFeeDate = trades.Min(t => t.TradeDate.AddDays(-backDateDividend5DaysJustInCase)); // eg: stockId = 1, TradeDate = 2022-12-13, firstBuyDate = 2022-12-15
            var maxTradeFeeDate = trades.Max(t => t.TradeDate);

            // Get Dividends and Trade Fees for the stock within the date range
            var dividends = await _dividendRepository.GetByStockAndDateRangeAsync(stockId, minDividendDate, maxDividendDate);
            var tradeFees = await _tradeFeeRepository.GetByStockAndDateRangeAsync(stockId, minTradeFeeDate, maxTradeFeeDate);

            // Calculate Profit and Loss
            ProfitAndLossCalculationResult plDto = ProfitAndLossCalculator.Calculate(trades, dividends, tradeFees);
            var pnl = new ProfitAndLoss
            {
                GrossProfit = plDto.GrossProfit,
                TotalDividends = plDto.TotalDividends,
                TotalFees = plDto.TotalFees,
                DaysHolding = plDto.DaysHolding,
                TradeTransactions = trades.ToList(),
                Dividends = dividends.ToList(),
                TradeFees = tradeFees.ToList()
            };

            await _profitAndLossRepository.AddAsync(pnl);

            // Link ProfitAndLossId to each Trade, Devidend & Fees
            foreach (var trade in trades)
                trade.ProfitAndLossId = pnl.Id;

            foreach (var dividend in dividends)
                dividend.ProfitAndLossId = pnl.Id;

            foreach (var fee in tradeFees)
                fee.ProfitAndLossId = pnl.Id;

            // Save all
            await _tradeTransactionRepository.UpdateRangeAsync(trades);
            await _dividendRepository.UpdateRangeAsync(dividends);
            await _tradeFeeRepository.UpdateRangeAsync(tradeFees);

            await transaction.CommitAsync();

            return pnl;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Error occurred in {Method} with TradeTransactionIds: {Ids}",
                nameof(ProcessProfitAndLossAsync), tradeTransactionIds);
            throw;
        }
    }

    public async Task<ProfitAndLoss?> GetByIdAsync(int id)
    {
        return await _profitAndLossRepository.FindOneAsync(x => x.Id == id,
            include: query => query.Include(u => u.TradeTransactions)
                                   .Include(v => v.Dividends)
                                   .Include(w => w.TradeFees));
    }

    //public async Task<ProfitAndLoss> ProcessProfitAndLossAsync(List<int> tradeTransactionIds)
    //{
    //    using var transaction = await _context.Database.BeginTransactionAsync();

    //    var trades = await _tradeTransactionRepository.GetByIdsAsync(tradeTransactionIds);
    //    if (!trades.Any())
    //        throw new InvalidOperationException("No trades found for the given IDs.");

    //    var stockId = trades.First().StockId;
    //    var minDate = trades.Min(t => t.TradeDate);
    //    var maxDate = trades.Max(t => t.TradeDate);

    //    var dividends = await _dividendRepository.GetByStockAndDateRangeAsync(stockId, minDate, maxDate);
    //    var tradeFees = await _tradeFeeRepository.GetByStockAndDateRangeAsync(stockId, minDate, maxDate);

    //    decimal totalBuy = trades.Where(t => !t.IsSold).Sum(t => t.TransactionAmount);
    //    decimal totalSell = trades.Where(t => t.IsSold).Sum(t => t.TransactionAmount);
    //    decimal totalFee = tradeFees.Sum(f => f.Amount);
    //    decimal totalDividends = dividends.Sum(d => d.Amount);

    //    var pnl = new ProfitAndLoss
    //    {
    //        GrossProfit = totalSell - totalBuy,
    //        TotalDividends = totalDividends,
    //        TotalFees = totalFee,
    //        DaysHolding = (maxDate - minDate).Days
    //    };

    //    await _profitAndLossRepository.AddAsync(pnl);

    //    // Link ProfitAndLossId to each Trade, Devidend & Fees
    //    foreach (var trade in trades)
    //        trade.ProfitAndLossId = pnl.Id;

    //    foreach (var dividend in dividends)
    //        dividend.ProfitAndLossId = pnl.Id;

    //    foreach (var fee in tradeFees)
    //        fee.ProfitAndLossId = pnl.Id;

    //    // Save all
    //    await _tradeTransactionRepository.UpdateRangeAsync(trades);
    //    await _dividendRepository.UpdateRangeAsync(dividends);
    //    await _tradeFeeRepository.UpdateRangeAsync(tradeFees);

    //    await transaction.CommitAsync();

    //    return pnl;
    //}
}
