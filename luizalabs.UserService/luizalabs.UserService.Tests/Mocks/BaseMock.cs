namespace luizalabs.UserService.Tests.Mocks;

using FluentValidation;
using Application;
using Application.Interface.Repository;
using Application.Interface.Service;
using Domain.Models.Users;
using Domain.Settings;
using Domain.Validation;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Mongo2Go;
using MongoDB.Driver;
using NUnit.Framework;

public class BaseMock
{
    protected static IUserService? UserService;
    protected CancellationTokenSource CancellationToken = new CancellationTokenSource();

    [OneTimeSetUp]
    public void SetUp()
    {
        var serviceProvider = ConfigureServices();

        UserService = ServiceProviderServiceExtensions.GetService<IUserService>(serviceProvider);
    }

    private static IServiceProvider ConfigureServices()
    {
        var runner = MongoDbRunner.Start();

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var services = new ServiceCollection();

        services.AddLogging();

        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

        var mongoClientSettings = MongoClientSettings.FromConnectionString(runner.ConnectionString);
        services.AddSingleton<IMongoClient>(new MongoClient(mongoClientSettings));
        services.AddSingleton(typeof(IBaseRepository<>), typeof(MongoBaseRepository<>));

        services.AddSingleton<IJwtService, JwtService>();
        services.AddSingleton<IUserService, UserService>();
        services.AddTransient<IValidator<CreateUserRequest>, CreateUserRequestValidator>();

        services.AddSingleton<ILoggerFactory, NullLoggerFactory>();

       return services.BuildServiceProvider();
    }
}