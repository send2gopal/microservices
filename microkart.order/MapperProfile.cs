using AutoMapper;
using microkart.order.Database;
using microkart.order.Models;

namespace microkart.order
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<OrderEntity, OrderResponse>()
                .ForMember(d => d.OrderStatus, opt => opt.MapFrom(s => new OrderStatus(s.OrderStatus, "Confirmed")));
            CreateMap<ShippingAddress, ShippingAddressResponse>();
            CreateMap<PaymentInformation, PaymentInformationResponse>();
            CreateMap<OrderItem, OrderItemResponse>();
        }
    }
}
