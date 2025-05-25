using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockSimulator.Business.Services;
using StockSimulator.Dtos.ProfitAndLoss;
using StockSimulator.Dtos.TradeTransaction.ReviewBuySellMatches;

namespace StockSimulator.Controllers;

[ApiController]
[Route("[controller]")]
public class StockAnalyticsController : ControllerBase
{
    private readonly IStockAnalyticsService _stockAnalyticsService;
    private readonly IMapper _mapper;

    //private readonly ILogger<WeatherForecastController> _logger;

    public StockAnalyticsController(IStockAnalyticsService stockAnalyticsService, IMapper mapper)
    {
        _stockAnalyticsService = stockAnalyticsService;
        _mapper = mapper;
    }

    [HttpGet("get-by-snapshotStockPriceGroupId", Name = "get-by-snapshotStockPriceGroupId")]
    public async Task<IActionResult> GetDataBySnapshotStockPriceGroupIdAsync(int snapshotStockPriceGroupId)
    {        
        var dtos = _mapper.Map<List<StockProfitAndLossDataDto>>(await _stockAnalyticsService.GetDataBySnapshotStockPriceGroupIdAsync(snapshotStockPriceGroupId));

        return Ok(dtos.OrderBy(x => x.StockName));
    }
}
