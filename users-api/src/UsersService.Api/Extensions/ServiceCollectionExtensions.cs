using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UsersService.Api.JsonConverters;
using UsersService.Business;
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
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        return services
            .AddDatabase(configuration)
            .AddDataServices()
            .AddBusinessServices()
            .AddAutoMapper()
            .AddValidation()
            .AddApiServices();
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, ConfigurationManager configuration) 
    {
        services.AddNpgsql<UsersDbContext>(configuration.GetConnectionString("UsersDatabase"));

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
