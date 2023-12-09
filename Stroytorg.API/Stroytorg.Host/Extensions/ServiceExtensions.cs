using Stroytorg.Domain.Data.Entities;
using Stroytorg.Infrastructure.Infrastructure;
using Stroytorg.Infrastructure.Infrastructure.Common;
using Stroytorg.Infrastructure.Store;

namespace Stroytorg.Host.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
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
}
