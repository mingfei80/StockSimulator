using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockSimulator.Business.Services;
using StockSimulator.Dtos;

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

    [HttpGet("get-by-stockId", Name = "tradefee-get-by-stockId")]
    public async Task<IActionResult> GetByStockId(int stockId)
    {        
        var dtos = _mapper.Map<List<UnassignedTradeFeeDto>>(await _tradeFeeService.GetByStockIdAsync(stockId));

        return Ok(dtos.OrderBy(x => x.TradeDate));
    }
}
