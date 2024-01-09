namespace luizalabs.UserService.API.Extensions;

using Dotnet.MiniJira.Infrastructure;
using luizalabs.UserService.Application;
using luizalabs.UserService.Application.Interface.Repository;
using luizalabs.UserService.Application.Interface.Service;
using luizalabs.UserService.Domain.Settings;
using MongoDB.Driver;

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
    }
}
