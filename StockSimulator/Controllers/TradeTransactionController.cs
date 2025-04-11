using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockSimulator.Business.Logic;
using StockSimulator.Business.Services;
using StockSimulator.Dtos;

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

    [HttpGet("get-by-stockIds", Name = "get-by-stockIds")]
    public async Task<IActionResult> GetTest()
    {
        int profitAndLossId = 1;
        List<int> stockIds = new List<int> { 1, 2, 3, 4, 5 };
        
        var stocksDTO = _mapper.Map<List<TradeTransactionDto>>(TradeMatcher.MatchAndCalculate(await _tradeTransactionService.GetByIdsAsync(stockIds), profitAndLossId));

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
