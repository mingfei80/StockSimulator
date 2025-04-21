using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockSimulator.Business.Services;
using StockSimulator.Dtos.TradeTransaction.ReviewBuySellMatches;

namespace StockSimulator.Controllers;

[ApiController]
[Route("[controller]")]
public class DividendController : ControllerBase
{
    private readonly IDividendService _dividendService;
    private readonly IMapper _mapper;

    //private readonly ILogger<WeatherForecastController> _logger;

    public DividendController(IDividendService dividendService, IMapper mapper)
    {
        _dividendService = dividendService;
        _mapper = mapper;
    }

    [HttpGet("get-by-stockId-with-profitAndLossId", Name = "dividend-get-by-stockId-with-profitAndLossId")]
    public async Task<IActionResult> GetByStockIdWithProfitAndLossId(int stockId, int buyerId, int? profitAndLossId)
    {        
        var dtos = _mapper.Map<List<DividendDto>>(await _dividendService.GetByStockIdWithProfitAndLossIdAsync(stockId, buyerId, profitAndLossId));

        return Ok(dtos.OrderBy(x => x.TradeDate));
    }
}
