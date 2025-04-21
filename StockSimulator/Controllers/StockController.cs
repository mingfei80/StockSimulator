using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockSimulator.Business.Logic;
using StockSimulator.Business.Services;
using StockSimulator.Dtos;
using StockSimulator.Dtos.TradeTransaction;

namespace StockSimulator.Controllers;

[ApiController]
[Route("[controller]")]
public class StockController : ControllerBase
{
    private readonly ITradeTransactionService _tradeTransactionService;
    private readonly IMapper _mapper;

    public StockController(ITradeTransactionService tradeTransactionService, IMapper mapper)
    {
        _tradeTransactionService = tradeTransactionService;
        _mapper = mapper;
    }

    //[HttpGet("get-unassigned-buy-sell-matches", Name = "get-unassigned-buy-sell-matches")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> GetUnassignedBuySellMatches(int buyerId)
    //{
    //    var stocksDTO = _mapper.Map<List<Dtos.TradeTransaction.UnassignedBuySellMatches.TradeTransactionDto>>(TradeMatcher.MatchAndCalculate(
    //                                                                    await _tradeTransactionService.GetStockUnassignedBuySellMatchesAsync(buyerId)
    //                                                                    ));

    //    var ids = stocksDTO.Where(x => x.ProfitAndLossId != null)
    //                        .OrderBy(x => x.StockId)
    //                        .Select(x => new Dtos.Stock.UnassignedBuySellMatches.StockDto
    //                        {
    //                            Id = x.StockId,
    //                            StockName = x.StockName ?? string.Empty
    //                        })
    //                        .DistinctBy(s => s.Id)
    //                        .ToList();

    //    return Ok(ids);
    //}
}
