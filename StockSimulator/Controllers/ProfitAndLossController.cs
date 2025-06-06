using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockSimulator.Business.Services;
using StockSimulator.Dtos.ProfitAndLoss;

namespace StockSimulator.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfitAndLossController : ControllerBase
{
    private readonly IProfitAndLossService _tradeAddProfitAndLossService;
    private readonly IMapper _mapper;

    //private readonly ILogger<WeatherForecastController> _logger;

    public ProfitAndLossController(IProfitAndLossService tradeAddProfitAndLossService, IMapper mapper)
    {
        _tradeAddProfitAndLossService = tradeAddProfitAndLossService;
        _mapper = mapper;
    }

    [HttpGet("get-by-id/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var plDto = _mapper.Map<ProfitAndLossDto>(await _tradeAddProfitAndLossService.GetByIdAsync(id));
            return Ok(plDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { Message = "An error occurred while processing your request.", Details = ex.Message });
        }
    }

    [HttpPost("group-using-sql", Name= "group-using-sql")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] List<int> tradeTransactionIds)
    {
        if (tradeTransactionIds == null || !tradeTransactionIds.Any())
            return BadRequest("Trade Transaction Ids are required.");

        try
        {
            var plDto = _mapper.Map<ProfitAndLossDto>(await _tradeAddProfitAndLossService.ProcessProfitAndLossAsync(tradeTransactionIds));

            if (plDto == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not calculate Profit and Loss.");

            return CreatedAtAction(nameof(GetById), new { id = plDto.Id }, plDto);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { Message = "An error occurred while processing your request.", Details = ex.Message });
        }
    }

    [HttpPost("group", Name = "group")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GroupProfitAndLoss([FromBody] Dtos.ProfitAndLoss.Group.GroupProfitAndLossRequest request)
    {
        if (request == null || !request.TradeTransactionIds.Any())
            return BadRequest("At least one TradeTransaction is required.");

        try
        {
            var plDto = _mapper.Map<ProfitAndLossDto>(
                                await _tradeAddProfitAndLossService.ProcessProfitAndLossAsync(request.TradeTransactionIds,
                                                                                                request.DividendIds,
                                                                                                request.TradeFeeIds));

            if (plDto == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not calculate Profit and Loss.");

            return CreatedAtAction(nameof(GetById), new { id = plDto.Id }, plDto);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { Message = "An error occurred while processing your request.", Details = ex.Message });
        }
    }
}
