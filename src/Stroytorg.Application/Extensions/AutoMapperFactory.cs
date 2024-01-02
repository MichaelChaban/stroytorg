using AutoMapper;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.Models.Order;
using DB = Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Application.Extensions;

public class AutoMapperFactory
{
    public DB.StroytorgDbContext? Context { get; private set; }

    public static IMapper CreateMapper()
    {
        MapperConfiguration config = CreateConfig();
        return config.CreateMapper();
    }

    public static MapperConfiguration CreateConfig()
    {
        return new MapperConfiguration(config =>
        {
            MapUser(config);
            MapOrder(config);
        });
    }

    private static void MapUser(IMapperConfigurationExpression config)
    {
        _ = config.CreateMap<DB.User, User>()
            .ForCtorParam(nameof(User.Profile), opt => opt.MapFrom(src => (int)src.Profile))
            .ForCtorParam(nameof(User.ProfileName), opt => opt.MapFrom(src => src.Profile.ToString()))
            .ReverseMap();

        _ = config.CreateMap<DB.User, UserRegister>()
                .ReverseMap();
    }

    private static void MapOrder(IMapperConfigurationExpression config)
    {
        _ = config.CreateMap<DB.Order, Order>()
            .ForCtorParam(nameof(Order.ShippingType), opt => opt.MapFrom(src => (int)src.ShippingType))
            .ForCtorParam(nameof(Order.ShippingTypeName), opt => opt.MapFrom(src => src.ShippingType.ToString()))
            .ForCtorParam(nameof(Order.PaymentType), opt => opt.MapFrom(src => (int)src.PaymentType))
            .ForCtorParam(nameof(Order.PaymentTypeName), opt => opt.MapFrom(src => src.PaymentType.ToString()))
            .ForCtorParam(nameof(Order.OrderStatus), opt => opt.MapFrom(src => (int)src.OrderStatus))
            .ForCtorParam(nameof(Order.OrderStatusName), opt => opt.MapFrom(src => src.OrderStatus.ToString()))
            .ReverseMap();
    }
}
