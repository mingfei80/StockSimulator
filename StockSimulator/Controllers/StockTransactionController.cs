using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockSimulator.Business.Services;
using StockSimulator.Data.Models;
using StockSimulator.Dtos;
using System.Threading.Tasks;

namespace StockSimulator.Controllers;

[ApiController]
[Route("[controller]")]
public class StockTransactionController : ControllerBase
{
    private readonly IStockTransactionService _stockTransactionService;
    private readonly IMapper _mapper;

    //private readonly ILogger<WeatherForecastController> _logger;

    public StockTransactionController(IStockTransactionService stockTransactionService, IMapper mapper)
    {
        _stockTransactionService = stockTransactionService;
        _mapper = mapper;
    }

    [HttpGet(Name = "get-all")]
    public async Task<IActionResult> GetAll()
    {
        var stocksDTO = _mapper.Map<List<StockTransactionDto>>(await _stockTransactionService.GetAllAsync());

        return Ok(new { StockTransactions = stocksDTO });
    }
}
