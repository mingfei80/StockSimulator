using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockSimulator.Business.Services;
using StockSimulator.Dtos.ProfitAndLoss;

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


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("get-by-snapshotStockPriceGroupId", Name = "get-by-snapshotStockPriceGroupId")]
    public async Task<IActionResult> GetDataBySnapshotStockPriceGroupIdAsync(int snapshotStockPriceGroupId)
    {
        try
        {
            if (snapshotStockPriceGroupId <= 0)
                return BadRequest("Invalid Snapshot Stock Price Group Id.");
            var result = _mapper.Map<StockProfitAndLossSummaryDto>(await _stockAnalyticsService.GetDataBySnapshotStockPriceGroupIdAsync(snapshotStockPriceGroupId));
            if (result == null)
                return NotFound("No data found for the provided Snapshot Stock Price Group Id.");


            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while processing your request.", Details = ex.Message });
        }
    }
}
