using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockSimulator.Business.Logic;
using StockSimulator.Business.Services;
using StockSimulator.Dtos;
using StockSimulator.Dtos.TradeTransaction;

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
        var stocksDTO = _mapper.Map<List<TradeTransactionDto>>(await _tradeTransactionService.GetAllAsync());

        return Ok(new { StockTransactions = stocksDTO });
    }

    /**
        C:\***StockSimulator\Useful-PrepareMatchedUnassignedStocksForProfitAndLoss.sql
        SQL above give you better data for
            PofitAndLossController
            - Post -
        {
            "stockIds": [1, 2, 3],
            "buyerId": 1,
            "startingProfitAndLossId": 0 --not important as this is preview only
        }
     */
    [HttpPost("unassigned-matched-stocks/prepare")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PrepareMatchedUnassignedStocksForProfitAndLoss([FromBody] MatchedStockRequestDto request)
    {
        var stocksDTO = _mapper.Map<List<TradeTransactionDto>>(TradeMatcher.MatchAndCalculate(
                                                                        await _tradeTransactionService.GetUassignedByStockIdAsync(request.StockIds, request.BuyerId), 
                                                                        request.StartingProfitAndLossId));

        var ids = stocksDTO.Where(x => x.ProfitAndLossId != null).OrderBy(x => x.StockId).Select(x => x.StockId).Distinct().ToList();

        return Ok(ids);
    }

    [HttpGet("get-by-stockId", Name = "get-by-stockId")]
    public async Task<IActionResult> GetByStockId(int stockId)
    {        
        var stocksDTO = _mapper.Map<List<TradeTransactionDto>>(await _tradeTransactionService.GetAllAsync());

        var ids = stocksDTO.Where(x => x.ProfitAndLossId == null && x.StockId == stockId).OrderBy(x => x.StockId).Select(x => x.StockId).Distinct().ToList();

        return Ok(ids);
    }
}
