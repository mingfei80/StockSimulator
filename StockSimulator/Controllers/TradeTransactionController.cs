using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockSimulator.Business.Logic;
using StockSimulator.Business.Services;
using StockSimulator.Dtos;
using StockSimulator.Dtos.TradeTransaction.ReviewBuySellMatches;

namespace StockSimulator.Controllers;

[ApiController]
[Route("[controller]")]
public class TradeTransactionController : ControllerBase
{
    private readonly ITradeTransactionService _tradeTransactionService;
    private readonly IMapper _mapper;

    //private readonly ILogger<WeatherForecastController> _logger;

    public TradeTransactionController(ITradeTransactionService tradeTransactionService, IMapper mapper)
    {
        _tradeTransactionService = tradeTransactionService;
        _mapper = mapper;
    }

    [HttpGet("get-all", Name = "get-all")]
    public async Task<IActionResult> GetAll()
    {
        var stocksDTO = _mapper.Map<List<Dtos.TradeTransactionDto>>(await _tradeTransactionService.GetAllAsync());

        return Ok(new { StockTransactions = stocksDTO });
    }

    ///**
    //    C:\***StockSimulator\Useful-PrepareMatchedUnassignedStocksForProfitAndLoss.sql
    //    SQL above give you better data for
    //        ProfitAndLossController
    //        - Post -
    //    {
    //        "stockIds": [1, 2, 3],
    //        "buyerId": 1,
    //        "startingProfitAndLossId": 0 --not important as this is preview only
    //    }
    // */
    [HttpGet("get-unassigned-buy-sell-matches", Name = "get-unassigned-buy-sell-matches")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUnassignedBuySellMatches(int buyerId)
    {
        var stocksDTO = _mapper.Map<List<Dtos.TradeTransaction.UnassignedBuySellMatches.TradeTransactionDto>>(TradeMatcher.MatchAndCalculate(
                                                                        await _tradeTransactionService.GetStockUnassignedBuySellMatchesAsync(buyerId)
                                                                        ));

        var ids = stocksDTO.Where(x => x.ProfitAndLossId != null)
                            .OrderBy(x => x.StockId)
                            .Select(x => new Dtos.TradeTransaction.UnassignedBuySellMatches.StockDto
                            {
                                Id = x.StockId,
                                StockName = x.StockName ?? string.Empty
                            })
                            .DistinctBy(s => s.Id)
                            .ToList();

        return Ok(ids);
    }

    [HttpGet("get-by-stockId-with-profitAndLossId", Name = "tradetransaction-get-by-stockId-with-profitAndLossId")]
    public async Task<IActionResult> GetByStockIdWithProfitAndLossId(int stockId, int buyerId, int? profitAndLossId)
    {
        var dtos = _mapper.Map<List<Dtos.TradeTransaction.ReviewBuySellMatches.TradeTransactionDto>>(await _tradeTransactionService.GetByStockIdWithProfitAndLossIdAsync(stockId, buyerId, profitAndLossId));

        return Ok(dtos.OrderBy(x => x.TradeDate));
    }
}
