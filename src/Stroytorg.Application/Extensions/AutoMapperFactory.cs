using AutoMapper;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.Models.Order;
using DbData = Stroytorg.Domain.Data.Entities;
using DbSpec = Stroytorg.Domain.Specifications;
using DbSort = Stroytorg.Domain.Sorting;
using DbEnum = Stroytorg.Domain.Data.Enums;
using ContractsSort = Stroytorg.Contracts.Sorting;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Category;
using Stroytorg.Contracts.Models.Material;
using Stroytorg.Contracts.Enums;
using Stroytorg.Application.Features.Materials.Commands;
using Stroytorg.Application.Features.Authentication.Commands;
using Stroytorg.Application.Features.Categories.Commands;

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
            MapCategory(config);
            MapMaterial(config);
            MapOrderMaterialMap(config);
            MapFilters(config);
            MapSoring(config);
            MapEnums(config);
        });
    }

    private static void MapUser(IMapperConfigurationExpression config)
    {
        _ = config.CreateMap<DbData.User, User>()
            .ForCtorParam(nameof(User.ProfileName), opt => opt.MapFrom(src => src.Profile.ToString()))
            .ForCtorParam(nameof(User.AuthenticationTypeName), opt => opt.MapFrom(src => src.AuthenticationType.ToString()));

        _ = config.CreateMap<DbData.User, RegisterCommand>()
            .ReverseMap();

        _ = config.CreateMap<DbData.User, UserRegister>()
            .ReverseMap();

        _ = config.CreateMap<UserRegister, RegisterCommand>()
            .ReverseMap();

        _ = config.CreateMap<DbData.User, GoogleAuthCommand>()
            .ReverseMap();

        _ = config.CreateMap<DbData.User, UserGoogleAuth>()
            .ReverseMap();

        _ = config.CreateMap<DbData.User, UserDetail>()
            .ForCtorParam(nameof(UserDetail.ProfileName), opt => opt.MapFrom(src => src.Profile.ToString()))
            .ForCtorParam(nameof(UserDetail.AuthenticationTypeName), opt => opt.MapFrom(src => src.AuthenticationType.ToString()));

        _ = config.CreateMap<UserRegister, DbData.User>();

        _ = config.CreateMap<UserGoogleAuth, DbData.User>();
    }

    private static void MapOrder(IMapperConfigurationExpression config)
    {
        _ = config.CreateMap<DbData.Order, Order>()
            .ForCtorParam(nameof(Order.ShippingTypeName), opt => opt.MapFrom(src => src.ShippingType.ToString()))
            .ForCtorParam(nameof(Order.PaymentTypeName), opt => opt.MapFrom(src => src.PaymentType.ToString()))
            .ForCtorParam(nameof(Order.OrderStatusName), opt => opt.MapFrom(src => src.OrderStatus.ToString()));

        _ = config.CreateMap<DbData.Order, OrderDetail>()
            .ForCtorParam(nameof(OrderDetail.ShippingTypeName), opt => opt.MapFrom(src => src.ShippingType.ToString()))
            .ForCtorParam(nameof(OrderDetail.PaymentTypeName), opt => opt.MapFrom(src => src.PaymentType.ToString()))
            .ForCtorParam(nameof(OrderDetail.OrderStatusName), opt => opt.MapFrom(src => src.OrderStatus.ToString()))
            .ForCtorParam(nameof(OrderDetail.Materials), opt => opt.MapFrom(src => src.OrderMaterialMap));

        _ = config.CreateMap<OrderCreate, DbData.Order>()
            .ForMember(nameof(DbData.Order.OrderMaterialMap), opt => opt.Ignore());
        _ = config.CreateMap<OrderEdit, DbData.Order>();
    }

    private static void MapCategory(IMapperConfigurationExpression config)
    {
        _ = config.CreateMap<DbData.Category, Category>();
        _ = config.CreateMap<DbData.Category, CategoryDetail>();

        _ = config.CreateMap<DbData.Category, CategoryEdit>()
            .ReverseMap();

        _ = config.CreateMap<DbData.Category, UpdateCategoryCommand>()
            .ForCtorParam(nameof(UpdateCategoryCommand.CategoryId), opt => opt.MapFrom(src => src.Id))
            .ReverseMap();

        _ = config.CreateMap<DbData.Category, CreateCategoryCommand>()
            .ReverseMap();
    }

    private static void MapMaterial(IMapperConfigurationExpression config)
    {
        _ = config.CreateMap<DbData.Material, Material>();
        _ = config.CreateMap<DbData.Material, MaterialDetail>();

        _ = config.CreateMap<DbData.Material, MaterialEdit>()
            .ReverseMap();

        _ = config.CreateMap<DbData.Material, UpdateMaterialCommand>()
            .ReverseMap();

        _ = config.CreateMap<CreateMaterialCommand, DbData.Material>()
            .ReverseMap();
    }

    private static void MapOrderMaterialMap(IMapperConfigurationExpression config)
    {
        _ = config.CreateMap<MaterialMapCreate, DbData.OrderMaterialMap>();

        _ = config.CreateMap<DbData.OrderMaterialMap, MaterialMap>()
                .ForMember(nameof(MaterialMap.Id), opt => opt.MapFrom(src => src.MaterialId));
    }

    private static void MapFilters(IMapperConfigurationExpression config)
    {
        _ = config.CreateMap<CategoryFilter, DbSpec.CategorySpecification>();
        _ = config.CreateMap<MaterialFilter, DbSpec.MaterialSpecification>();
        _ = config.CreateMap<OrderFilter, DbSpec.OrderSpecification>();
    }

    private static void MapSoring(IMapperConfigurationExpression config)
    {
        _ = config.CreateMap<ContractsSort.SortDefinition, DbSort.Common.SortDefinition>();
    }

    private static void MapEnums(IMapperConfigurationExpression config)
    {
        _ = config.CreateMap<OrderStatus, DbEnum.OrderStatus>()
            .ReverseMap();
        _ = config.CreateMap<PaymentType, DbEnum.PaymentType>()
            .ReverseMap();
        _ = config.CreateMap<ShippingType, DbEnum.ShippingType>()
            .ReverseMap();
    }
}
