﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
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
            .AddAutoMapper()
            .AddMicroservices()
            .AddFacades()
            .AddRepositories()
            .AddDb();
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
        services.TryAddScoped<IUserService, UserService>();
        services.TryAddScoped<IAuthService, AuthService>();
        services.TryAddScoped<ITokenGeneratorService, TokenGeneratorService>();
        services.TryAddScoped<ICategoryService, CategoryService>();
        services.TryAddScoped<IMaterialService, MaterialService>();
        services.TryAddScoped<IOrderService, OrderService>();

        return services;
    }

    private static IServiceCollection AddFacades(this IServiceCollection services)
    {
        services.TryAddScoped<IOrderServiceFacade, OrderServiceFacade>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.TryAddScoped<IUserContext, UserContext>();
        services.TryAddScoped<IUserRepository, UserRepository>();
        services.TryAddScoped<ICategoryRepository, CategoryRepository>();
        services.TryAddScoped<IMaterialRepository, MaterialRepository>();
        services.TryAddScoped<IOrderRepository, OrderRepository>();

        return services;
    }

    private static IServiceCollection AddDb(this IServiceCollection services)
    {
        services.TryAddScoped<IUnitOfWork, StroytorgDbContext>();
        services.AddSingleton<IDatabaseConnectionString, ConnectionStringConfig>();
        services.TryAddScoped<IStroytorgDbContext, StroytorgDbContext>();

        return services;
    }
}
