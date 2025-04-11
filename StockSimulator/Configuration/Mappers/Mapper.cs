using AutoMapper;
using StockSimulator.Data.Models;
using StockSimulator.Dtos;
using StockSimulator.Dtos.ProfitAndLoss;

namespace StockSimulator.Configuration.Mappers;

public class StockSimulatorProfile : Profile
{
    public StockSimulatorProfile()
    {
        CreateMap<ProfitAndLoss, ProfitAndLossDto>()
            .ForMember(dest => dest.StockName, opt => opt.MapFrom(src => src.TradeTransactions.First().Stock.StockName));
            //.ForMember(dest => dest.TradeFees, opt => opt.MapFrom(src => src.TradeTransactions))
            //.ForMember(dest => dest.Dividends, opt => opt.MapFrom(src => src.Dividends))
            //.ForMember(dest => dest.TradeFees, opt => opt.MapFrom(src => src.TradeFees));
        CreateMap<TradeFee, UnassignedTradeFeeDto>();
        CreateMap<Dividend, UnassignedDividendDto>();
        CreateMap<TradeTransaction, Dtos.TradeTransactionDto>();
        CreateMap<TradeTransaction, UnassignedTradeTransactionDto>()
            .ForMember(dest => dest.StockName, opt => opt.MapFrom(src => src.Stock.StockName));
        ///.ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock));
    }
}
//public class StockTransactionService
//{
//    private readonly IMapper _mapper;

//    public StockTransactionService(IMapper mapper)
//    {
//        _mapper = mapper;
//    }

//    public StockTransactionDto GetTransactionDto(StockTransaction transaction)
//    {
//        return _mapper.Map<StockTransactionDto>(transaction);
//    }
//}
