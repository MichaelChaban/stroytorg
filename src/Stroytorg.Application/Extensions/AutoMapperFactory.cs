using AutoMapper;
using Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Application.Extensions;

public class AutoMapperFactory
{
    public StroytorgDbContext Context { get; private set; }

    public static IMapper CreateMapper()
    {
        MapperConfiguration config = CreateConfig();
        return config.CreateMapper();
    }

    public static MapperConfiguration CreateConfig()
    {
        return new MapperConfiguration(cfg =>
        {
            
        });
    }
}
