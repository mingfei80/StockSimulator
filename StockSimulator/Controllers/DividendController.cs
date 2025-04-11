using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockSimulator.Business.Services;
using StockSimulator.Dtos;

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

    [HttpGet("get-by-stockId", Name = "dividend-get-by-stockId")]
    public async Task<IActionResult> GetByStockId(int stockId)
    {        
        var dtos = _mapper.Map<List<UnassignedDividendDto>>(await _dividendService.GetByStockIdAsync(stockId));

        return Ok(dtos.OrderBy(x => x.TradeDate));
    }
}
