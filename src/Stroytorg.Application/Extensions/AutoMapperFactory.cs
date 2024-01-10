using AutoMapper;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.Models.Order;
using DbData = Stroytorg.Domain.Data.Entities;
using DbSpec = Stroytorg.Domain.Specifications;
using DbSort = Stroytorg.Domain.Sorting;
using Stroytorg.Contracts.Filters;
using ContractsSort = Stroytorg.Contracts.Sorting;

namespace Stroytorg.Application.Extensions;

public class AutoMapperFactory
{
    public DbData.StroytorgDbContext? Context { get; private set; }

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
            MapFilters(config);
            MapSoring(config);
        });
    }

    private static void MapUser(IMapperConfigurationExpression config)
    {
        _ = config.CreateMap<DbData.User, User>()
            .ForCtorParam(nameof(User.Profile), opt => opt.MapFrom(src => (int)src.Profile))
            .ForCtorParam(nameof(User.ProfileName), opt => opt.MapFrom(src => src.Profile.ToString()))
            .ForCtorParam(nameof(User.AuthenticationType), opt => opt.MapFrom(src => (int)src.AuthenticationType))
            .ForCtorParam(nameof(User.AuthenticationTypeName), opt => opt.MapFrom(src => src.AuthenticationType.ToString()))
            .ReverseMap();

        _ = config.CreateMap<DbData.User, UserRegister>()
                .ReverseMap();

        _ = config.CreateMap<DbData.User, UserGoogleAuth>()
                .ReverseMap();
    }

    private static void MapOrder(IMapperConfigurationExpression config)
    {
        _ = config.CreateMap<DbData.Order, Order>()
            .ForCtorParam(nameof(Order.ShippingType), opt => opt.MapFrom(src => (int)src.ShippingType))
            .ForCtorParam(nameof(Order.ShippingTypeName), opt => opt.MapFrom(src => src.ShippingType.ToString()))
            .ForCtorParam(nameof(Order.PaymentType), opt => opt.MapFrom(src => (int)src.PaymentType))
            .ForCtorParam(nameof(Order.PaymentTypeName), opt => opt.MapFrom(src => src.PaymentType.ToString()))
            .ForCtorParam(nameof(Order.OrderStatus), opt => opt.MapFrom(src => (int)src.OrderStatus))
            .ForCtorParam(nameof(Order.OrderStatusName), opt => opt.MapFrom(src => src.OrderStatus.ToString()))
            .ReverseMap();
    }

    private static void MapFilters(IMapperConfigurationExpression config)
    {
        _ = config.CreateMap<CategoryFilter, DbSpec.CategorySpecification>();
    }

    private static void MapSoring(IMapperConfigurationExpression config)
    {
        _ = config.CreateMap<ContractsSort.SortDefinition, DbSort.Common.SortDefinition>();
    }
}
