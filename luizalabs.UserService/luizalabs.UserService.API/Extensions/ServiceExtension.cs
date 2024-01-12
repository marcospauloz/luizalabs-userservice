namespace luizalabs.UserService.API.Extensions;

using Application;
using Application.Interface.Repository;
using Application.Interface.Service;
using Domain.Settings;
using MongoDB.Driver;
using FluentValidation;
using Domain.Models.Users;
using Domain.Validation;
using Infrastructure;

public static class ServiceExtension
{
    public static void ConfigureService(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

        var mongoClientSettings = MongoClientSettings.FromConnectionString(builder.Configuration.GetConnectionString("Mongo"));
        services.AddSingleton<IMongoClient>(new MongoClient(mongoClientSettings));
        services.AddSingleton(typeof(IBaseRepository<>), typeof(MongoBaseRepository<>));

        services.AddSingleton<IJwtService, JwtService>();
        services.AddSingleton<IUserService, UserService>();
        
        services.AddTransient<IValidator<CreateUserRequest>, CreateUserRequestValidator>();
    }
}
