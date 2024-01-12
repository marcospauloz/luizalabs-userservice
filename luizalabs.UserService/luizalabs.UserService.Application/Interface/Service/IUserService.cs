namespace luizalabs.UserService.Application.Interface.Service;

using Domain.Entities;
using Domain.Models.Users;

public interface IUserService
{
    public Task<AuthenticatedResponse> AuthenticateAsync(AuthenticateRequest model, CancellationToken cancellationToken);

    public Task<AuthenticatedResponse> CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken);

    public Task<User> GetByIdAsync(string id, CancellationToken cancellationToken);
}
