using System;
using AutoMapper;
using NinjaSaiGon.Order.Models.ShoppingCartViewModels;

namespace NinjaSaiGon.Order.AutoMapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<CreateOrderModel, Data.Models.Order>()
                .ForMember(order => order.OrderDeliveried, opt => opt.MapFrom(src => src.IsDeliveryNow ? DateTime.Now.AddMinutes(40) : src.OrderDeliveried))
                //.ForMember(order => order.AddressLine, opt => opt.MapFrom(src => $"{src.AddressLine}, {src.District}, {src.City}"))
                .ForMember(order => order.ShipFee, opt => opt.MapFrom(src => src._se_am))
                .ForMember(order => order.Distance, opt => opt.MapFrom(src => src._de_lg))
                .ForMember(order => order.DiscountAmount, opt => opt.MapFrom(src => src._dt_am));
        }
    }
}
