using AutoMapper;
using Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Application.Extensions;

public class AutoMapperFactory
{
    public StroytorgDbContext? Context { get; private set; }

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
        });
    }

    private static void MapUser(IMapperConfigurationExpression config)
    {
        _ = config.CreateMap<User, Contracts.Models.User>()
                .ReverseMap();
    }
}
