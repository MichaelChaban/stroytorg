using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stroytorg.Application;
using Stroytorg.Application.Behaviors;
using Stroytorg.Application.Extensions;
using Stroytorg.Application.Facades;
using Stroytorg.Application.Facades.Interfaces;
using Stroytorg.Application.Services;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Domain.Data.Entities;
using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Repositories;
using Stroytorg.Domain.Data.Repositories.Common;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Host.Middlewares;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using Stroytorg.Infrastructure.Configuration;
using Stroytorg.Infrastructure.Configuration.Interfaces;
using Stroytorg.Infrastructure.Store;
using System.Text;

namespace Stroytorg.Host.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpContextAccessor()
            .AddMiddlewares()
            .AddMediatR()
            .AddValidators()
            .AddAutoMapper()
            .AddMicroservices()
            .AddFacades()
            .AddRepositories()
            .AddDb()
            .AddSwagger();
    }

    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddSingleton<IJwtSettings, JwtSettingsConfig>();
        var secret = Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]!);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidateActor = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(secret),
                ClockSkew = TimeSpan.Zero,
            };
            options.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = (context) => { return Task.CompletedTask; },
            };
        });

        services.AddAuthorization(c =>
        {
            c.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).Build();
        });
    }

    public static void MigrateDatabase(this IApplicationBuilder app)
    {
        var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
        if (scopeFactory is null)
        {
            return;
        }

        using var scope = scopeFactory.CreateScope();
        scope.ServiceProvider.GetRequiredService<IStroytorgDbContext>().Migrate();
    }

    private static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddResponseCaching();
        IMapper mapper = AutoMapperFactory.CreateMapper();
        services.TryAddSingleton(mapper);
        services.TryAddScoped<IAutoMapperTypeMapper, AutoMapperTypeMapper>();

        return services;
    }

    private static IServiceCollection AddMicroservices(this IServiceCollection services)
    {
        services.TryAddScoped<ITokenGeneratorService, TokenGeneratorService>();

        return services;
    }

    private static IServiceCollection AddFacades(this IServiceCollection services)
    {
        services.TryAddScoped<IOrderFacade, OrderFacade>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.TryAddScoped<IUserContext, UserContext>();
        services.TryAddScoped<IUserRepository, UserRepository>();
        services.TryAddScoped<ICategoryRepository, CategoryRepository>();
        services.TryAddScoped<IMaterialRepository, MaterialRepository>();
        services.TryAddScoped<IOrderRepository, OrderRepository>();
        services.TryAddScoped<IOrderMaterialMapRepository, OrderMaterialMapRepository>();

        return services;
    }

    private static IServiceCollection AddDb(this IServiceCollection services)
    {
        services.TryAddScoped<IUnitOfWork, StroytorgDbContext>();
        services.AddSingleton<IDatabaseConnectionString, ConnectionStringConfig>();
        services.TryAddScoped<IStroytorgDbContext, StroytorgDbContext>();

        return services;
    }

    private static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.TryAddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);
        return services;
    }

    private static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Stroytorg.API", Version = "v1" });
        });

        return services;
    }
}