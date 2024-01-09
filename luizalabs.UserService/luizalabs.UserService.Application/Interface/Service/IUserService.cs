namespace luizalabs.UserService.Application.Interface.Service;

using Dotnet.MiniJira.Domain.Models.Users;
using luizalabs.UserService.Domain.Entities;
using luizalabs.UserService.Domain.Models.Users;

public interface IUserService
{
    public Task<AuthenticatedResponse> AuthenticateAsync(AuthenticateRequest model, CancellationToken cancellationToken);

    public Task<AuthenticatedResponse> CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken);

    public Task<User> GetByIdAsync(string id, CancellationToken cancellationToken);
}
