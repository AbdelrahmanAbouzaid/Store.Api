using AutoMapper;
using OrderAddress = Domain.Models.OrderModels.Address;
using UserAddress = Domain.Models.Identity.Address;
using Shared.OrderModels;
using Domain.Models.OrderModels;

namespace Services.MappingProiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<UserAddress, AddressDto>().ReverseMap();
            CreateMap<OrderAddress, AddressDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl));

            CreateMap<Order, OrderResultDto>()
                .ForMember(d => d.PaymentStatus, s => s.MapFrom(o => o.PaymentStatus.ToString()))
                .ForMember(d => d.DeliveryMethod, s => s.MapFrom(o => o.DeliveryMethod.ShortName))
                .ForMember(d => d.Total, s => s.MapFrom(o => o.SubTotal + o.DeliveryMethod.Cost));

            CreateMap<DeliveryMethod, DeliveryMethodsDto>();
        }
    }
}
