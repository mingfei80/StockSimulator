using AutoMapper;
using StockSimulator.Data.Models;
using StockSimulator.Data.Models.Projection;
using StockSimulator.Dtos.ProfitAndLoss;

namespace StockSimulator.Configuration.Mappers;

public class StockSimulatorProfile : Profile
{
    public StockSimulatorProfile()
    {
        CreateMap<ProfitAndLoss, ProfitAndLossDto>()
            .ForMember(dest => dest.StockName, opt => opt.MapFrom(src => src.TradeTransactions.First().Stock.StockName))
            .ForMember(dest => dest.TradeTransactions, opt => opt.MapFrom(src => src.TradeTransactions))
            .ForMember(dest => dest.Dividends, opt => opt.MapFrom(src => src.Dividends))
            .ForMember(dest => dest.TradeFees, opt => opt.MapFrom(src => src.TradeFees));
        CreateMap<TradeTransaction, Dtos.ProfitAndLoss.TradeTransactionDto>();
        CreateMap<TradeFee, Dtos.ProfitAndLoss.TradeFeeDto>();
        CreateMap<Dividend, Dtos.ProfitAndLoss.DividendDto>();

        CreateMap<TradeTransaction, Dtos.TradeTransactionDto>();

        CreateMap<TradeFee, Dtos.TradeTransaction.ReviewBuySellMatches.TradeFeeDto>();
        CreateMap<Dividend, Dtos.TradeTransaction.ReviewBuySellMatches.DividendDto>();
        CreateMap<TradeTransaction, Dtos.TradeTransaction.ReviewBuySellMatches.TradeTransactionDto>()
            .ForMember(dest => dest.StockName, opt => opt.MapFrom(src => src.Stock.StockName));

        CreateMap<TradeTransaction, Dtos.TradeTransaction.UnassignedBuySellMatches.TradeTransactionDto>()
            .ForMember(dest => dest.StockName, opt => opt.MapFrom(src => src.Stock.StockName));


        CreateMap<StockProfitAndLossData, StockProfitAndLossDataDto>()
            .ForMember(dest => dest.SellTransactions, opt => opt.MapFrom(src => src.SellTransactions))
            .ForMember(dest => dest.BuyTransactions, opt => opt.MapFrom(src => src.BuyTransactions))
            .ForMember(dest => dest.Fees, opt => opt.MapFrom(src => src.Fees))
            .ForMember(dest => dest.Dividends, opt => opt.MapFrom(src => src.Dividends));
        CreateMap<StockProfitAndLossSummaryResult, StockProfitAndLossSummaryDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.GrandTotalGrossProfit, opt => opt.MapFrom(src => src.GrandTotalGrossProfit))
            .ForMember(dest => dest.GrandTotalDividends, opt => opt.MapFrom(src => src.GrandTotalDividends))
            .ForMember(dest => dest.GrandTotalFees, opt => opt.MapFrom(src => src.GrandTotalFees));

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
