using System.Text;
using Application.Interfaces;
using Application.Services;
using Domain.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Context;
using Presentation.Constants;
using Serilog;

namespace Presentation.Extensions;

public static class StartupExtensions
{
    public static IServiceCollection AddAirportDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<AirportContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(ConfigurationConstants.DefaultConnectionString) ?? throw new InvalidOperationException("EmptyConnectionString")));
        return serviceCollection;
    }
    
    public static IServiceCollection AddServicesOptions(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var authOptionsSection = configuration.GetSection(ConfigurationConstants.AuthenticationSection);
        serviceCollection.Configure<AuthOptions>(authOptionsSection);
        return serviceCollection;
    }
    
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var authOptionsSection = configuration.GetSection(ConfigurationConstants.AuthenticationSection);
        var authOptions = authOptionsSection.Get<AuthOptions>();
        
        serviceCollection.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var key = Encoding.UTF8.GetBytes(authOptions!.Key);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = authOptions.Issuer,
                ValidAudience = authOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
        return serviceCollection;
    }
    
    public static IServiceCollection AddSwaggerSecurity(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });

            c.SwaggerDoc(name: "v1", new OpenApiInfo
            {
                Title = "Airport",
                Version = "v1"
            });
        });
    }
    
    public static IServiceCollection AddCustomServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ITokenProvider, TokenProvider>();
        return serviceCollection;
    }
    
    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConfig = configuration.GetSection(ConfigurationConstants.RedisCacheConfigurationSection).Get<RedisOptions>();
        services.AddSingleton(redisConfig);

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConfig.ConnectionString;
            options.InstanceName = redisConfig.InstanceName;
        });

        return services;
    }
    
    public static IHostBuilder UseCustomSerilog(this IHostBuilder hostBuilder)
    {
        return hostBuilder.UseSerilog((context, config) =>
        {
            var logConnectionString = context.Configuration.GetConnectionString(ConfigurationConstants.LoggingConnectionString);
            config.WriteTo.PostgreSQL(connectionString: logConnectionString, tableName: ConfigurationConstants.LoggingTableName,
                    needAutoCreateTable: true)
                .MinimumLevel.Information();
            if (context.HostingEnvironment.IsDevelopment())
            {
                config.WriteTo.Console().MinimumLevel.Information();
            }
        });
    }
}