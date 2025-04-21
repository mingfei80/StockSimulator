using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockSimulator.Business.Services;
using StockSimulator.Dtos.TradeTransaction.ReviewBuySellMatches;

namespace StockSimulator.Controllers;

[ApiController]
[Route("[controller]")]
public class TradeFeeController : ControllerBase
{
    private readonly ITradeFeeService _tradeFeeService;
    private readonly IMapper _mapper;

    //private readonly ILogger<WeatherForecastController> _logger;

    public TradeFeeController(ITradeFeeService tradeFeeService, IMapper mapper)
    {
        _tradeFeeService = tradeFeeService;
        _mapper = mapper;
    }

    [HttpGet("get-by-stockId-with-profitAndLossId", Name = "tradefee-get-by-stockId-with-profitAndLossId")]
    public async Task<IActionResult> GetByStockIdWithProfitAndLossIdAsync(int stockId, int buyerId, int? profitAndLossId)
    {        
        var dtos = _mapper.Map<List<TradeFeeDto>>(await _tradeFeeService.GetByStockIdWithProfitAndLossIdAsync(stockId, buyerId, profitAndLossId));

        return Ok(dtos.OrderBy(x => x.TradeDate));
    }
}
