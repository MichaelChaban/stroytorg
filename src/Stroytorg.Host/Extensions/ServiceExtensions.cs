using AutoMapper;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Stroytorg.Application.Extensions;
using Stroytorg.Application.Services;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Domain.Data.Entities;
using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Repositories;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using Stroytorg.Infrastructure.Infrastructure;
using Stroytorg.Infrastructure.Infrastructure.Common;
using Stroytorg.Infrastructure.Store;

namespace Stroytorg.Host.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddAutoMapper();
        services.AddMicroservices();
        services.AddDb();
        services.AddRepositories();
    }

    public static void MigrateDatabase(this IApplicationBuilder app)
    {
        var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
        if (scopeFactory != null)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<IStroytorgDbContext>().Migrate();
            }
        }
    }

    private static void AddMicroservices(this IServiceCollection services)
    {
        services.TryAddScoped<IUserService, UserService>();
    }

    private static void AddDb(this IServiceCollection services)
    {
        services.TryAddScoped<IUnitOfWork, StroytorgDbContext>();
        services.AddSingleton<IDatabaseConnectionString, ConnectionStringConfig>();
        services.TryAddScoped<IStroytorgDbContext, StroytorgDbContext>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.TryAddScoped<IUserContext, UserContext>();
        services.TryAddScoped<IUserRepository, UserRepository>();
    }

    private static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddResponseCaching();
        IMapper mapper = AutoMapperFactory.CreateMapper();
        services.TryAddSingleton(mapper);
        services.TryAddScoped<IAutoMapperTypeMapper, AutoMapperTypeMapper>();
    }
}
