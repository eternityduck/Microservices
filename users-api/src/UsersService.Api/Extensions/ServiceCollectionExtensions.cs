using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using UsersService.Api.JsonConverters;
using UsersService.Business;
using UsersService.Business.Communication;
using UsersService.Business.Interfaces.Communication;
using UsersService.Business.Interfaces.Services;
using UsersService.Business.Services;
using UsersService.Data;
using UsersService.Data.Interfaces;
using UsersService.Data.Interfaces.Repositories;
using UsersService.Data.Persistence;
using UsersService.Data.Repositories;

namespace UsersService.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices
        (this IServiceCollection services, ConfigurationManager configuration, IWebHostEnvironment environment)
    {
        return services
            .AddDatabase(configuration, environment)
            .AddDataServices()
            .AddBusinessServices()
            .AddAutoMapper()
            .AddValidation()
            .AddApiServices();
    }

    private static IServiceCollection AddDatabase
        (this IServiceCollection services, ConfigurationManager configuration, IWebHostEnvironment environment) 
    {
        string? connectionString;
        
        if (environment.IsDevelopment())
        {
            connectionString = configuration.GetConnectionString("UsersDatabase");
        }
        else
        {
            var pgHost = Environment.GetEnvironmentVariable("POSTGRES_HOST");
            var pgPort = Environment.GetEnvironmentVariable("POSTGRES_PORT");
            var pgDatabase = Environment.GetEnvironmentVariable("POSTGRES_DB");
            var pgUser = Environment.GetEnvironmentVariable("POSTGRES_USER");
            var pgPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

            connectionString = $"Host={pgHost};Port={pgPort};Database={pgDatabase};Username={pgUser};Password={pgPassword}";
        }

        services.AddNpgsql<UsersDbContext>(connectionString);

        return services;
    }

    private static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddHttpContextAccessor();

        services.AddHttpClient<IProductsHttpClient, ProductsHttpClient>().ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        });

        return services;
    }

    private static IServiceCollection AddAutoMapper(this IServiceCollection services)
        => services.AddAutoMapper(typeof(AutoMapperBusinessProfile), typeof(AutoMapperApiProfile));

    private static IServiceCollection AddValidation(this IServiceCollection services)
        => services.AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<Program>();

    private static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        services.AddControllers()
            .AddNewtonsoftJson()
            .AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new DateOnlyNullableJsonConverter());
            });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date"
            });
        });

        services.AddCors(setup =>
        {
            setup.AddPolicy("default", options =>
            {
                options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().WithExposedHeaders("content-disposition");
            });
        });

        return services;
    }
}
