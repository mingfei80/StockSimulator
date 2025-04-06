namespace StockSimulator.Configuration.Mappers;

using AutoMapper;
using StockSimulator.Data.Models;
using StockSimulator.Dtos;
using System;

public class StockTransactionProfile : Profile
{
    public StockTransactionProfile()
    {
        CreateMap<StockTransaction, StockTransactionDto>()
            .ForMember(dest => dest.StockName, opt => opt.MapFrom(src => src.Stock.StockName))
            .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.Agent.AgentName))
            .ForMember(dest => dest.BuyerName, opt => opt.MapFrom(src => src.Buyer.BuyerName));
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
