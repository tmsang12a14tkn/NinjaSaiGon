using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NinjaSaiGon.Admin.Models;
using NinjaSaiGon.Admin.Models.DrinkViewModels;
using NinjaSaiGon.Admin.Models.OrderModels;
using NinjaSaiGon.Data.Models;

namespace NinjaSaiGon.Admin.AutoMapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<EditOrderInfoModel, Order>().ForMember(order => order.OrderDetails, opt => opt.Ignore());
            CreateMap<EditOrderDetailModel, OrderDetail>();
            CreateMap<EditOrderCustomerModel, Order>();
            CreateMap<EditOrderNotesModel, Order>();
            CreateMap<EditOrderDeliveryModel, OrderDelivery>();
            CreateMap<OrderDetailToppingModel, OrderDetailTopping>();

            CreateMap<OrderDetail, EditOrderDetailModel>();
            CreateMap<OrderDetailTopping, OrderDetailToppingModel>();

            CreateMap<OrderDetail, OrderDetailView>();
            CreateMap<Order, OrderView>().ForMember(des=>des.FullName, opt => opt.MapFrom(src => String.Join(" ", new string[] {src.LastName, src.MiddleName, src.FirstName}.Where(s => !string.IsNullOrEmpty(s)))));

            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderDelivery, OrderDeliveryViewModel>();
            CreateMap<OrderDetail, CartDrinkView>().ForMember(des => des.Toppings, opt => opt.MapFrom(src => src.OrderDetailToppings));
            CreateMap<OrderDetailTopping, CartToppingView>().ForMember(des => des.Topping, opt => opt.MapFrom(src => src.ToppingName));
            
            CreateMap<OrderViewModel, Order>().ForMember(des => des.OrderDetails, opt=>opt.Ignore());
            CreateMap<OrderDeliveryViewModel, OrderDelivery>();
            CreateMap<CartDrinkView, OrderDetail>().ForMember(des => des.OrderDetailToppings, opt => opt.MapFrom(src => src.Toppings)).ForMember(dest => dest.OrderDetailId, opt => opt.Ignore());
            CreateMap<CartToppingView, OrderDetailTopping>().ForMember(des => des.ToppingName, opt => opt.MapFrom(src => src.Topping));

            CreateMap<DrinkOrderingViewModel, IceOption>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<DrinkOrderingViewModel, SugarOption>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<DrinkOrderingViewModel, DrinkSize>().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<EditDrinkModel, Drink>();
            CreateMap<EditDrinkPhotoModel, DrinkPhoto>();
        }
    }
}
