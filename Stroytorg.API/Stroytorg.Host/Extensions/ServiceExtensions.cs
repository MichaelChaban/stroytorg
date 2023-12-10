using AutoMapper;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Stroytorg.Application.Extensions;
using Stroytorg.Application.Services;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Domain.Data.Entities;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using Stroytorg.Infrastructure.Infrastructure;
using Stroytorg.Infrastructure.Infrastructure.Common;
using Stroytorg.Infrastructure.Store;

namespace Stroytorg.Host.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterAutoMapper();

        services.AddMicroservices();
        services.AddScoped<IUnitOfWork, StroytorgDbContext>();
        services.AddSingleton<IDatabaseConnectionString, ConnectionStringConfig>();
        services.AddScoped<StroytorgDbContext>();
    }

    public static void MigrateDatabase(this IApplicationBuilder app)
    {
        var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
        if (scopeFactory != null)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<StroytorgDbContext>().Migrate();
            }
        }
    }

    private static void AddMicroservices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }

    private static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddResponseCaching();
        IMapper mapper = AutoMapperFactory.CreateMapper();
        _ = services.AddSingleton(mapper);
        services.TryAddScoped<IAutoMapperTypeMapper, AutoMapperTypeMapper>();
    }
}
