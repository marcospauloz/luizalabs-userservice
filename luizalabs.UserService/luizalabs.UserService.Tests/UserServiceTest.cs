namespace luizalabs.UserService.Tests;

using Mocks;
using NUnit.Framework;

[TestFixture]
public class UserServiceTest : BaseMock
{
    [Test]
    public async Task OnCreateUser_ShouldCreateUserWithSuccess()
    {
        //Arrange
        var userToCreate = new Domain.Models.Users.CreateUserRequest
        {
            Email = $"test@test.com",
            Name = $"Test",
            Password = $"Abc1234",
        };

        //Act
        var result = await UserService!.CreateUserAsync(userToCreate, CancellationToken.Token);
        
        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Email, Is.EqualTo(userToCreate.Email));
        Assert.That(userToCreate.Password, Is.Not.Null);
    }
}