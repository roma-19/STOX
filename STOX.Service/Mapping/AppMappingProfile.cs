using AutoMapper;
using STOX.Data.Entities;
using STOX.Data.Enums;
using STOX.Service.DTOs;
using STOX.Service.DTOs.User;

namespace STOX.Service.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Cart, CartDto>().ReverseMap();
        
        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ReverseMap();
        
        CreateMap<Notification,  NotificationDto>().ReverseMap();

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ReverseMap();
        
        CreateMap<Orders,  OrdersDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<OrderStatus>(src.Status)));

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => Enum.Parse<Category>(src.Category)));
        
        CreateMap<Review, ReviewDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ReverseMap();
        
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse<Role>(src.Role)));

        CreateMap<RegisterUserDto, User>();
    }
}