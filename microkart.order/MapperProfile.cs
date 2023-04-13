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
                .ForMember(d => d.OrderStatus, opt => opt.MapFrom(s => new OrderStatus(s.OrderStatus, getOrderStatus(s.OrderStatus))));
            CreateMap<ShippingAddress, ShippingAddressResponse>();
            CreateMap<PaymentInformation, PaymentInformationResponse>();
            CreateMap<OrderItem, OrderItemResponse>();
        }

        private static string getOrderStatus(int status)
        {
            switch (status)
            {
                case 0:
                    return "New";
                case 1:
                    return "Awaiting Validation";
                case 2:
                case 3:
                    return "Confirmed";
                case 4:
                    return "Shipped";
                case 5:
                    return "Cancelled";
                case 6:
                    return "Payment Failed";
                case 7:
                    return "Delivered";
                default:
                    return "Failed";
            }
        }
    }
}
