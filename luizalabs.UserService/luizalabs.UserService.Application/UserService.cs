namespace luizalabs.UserService.Application;

using BCrypt.Net;
using Dotnet.MiniJira.Domain.Helpers;
using Dotnet.MiniJira.Domain.Models.Users;
using luizalabs.UserService.Application.Interface.Repository;
using luizalabs.UserService.Application.Interface.Service;
using luizalabs.UserService.Domain.Core.Exceptions;
using luizalabs.UserService.Domain.Entities;
using luizalabs.UserService.Domain.Models.Users;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly IBaseRepository<User> userRepository;
    private readonly IJwtService jwtService;

    public UserService(IBaseRepository<User> userRepository, IJwtService jwtService)
    {
        this.userRepository = userRepository;
        this.jwtService = jwtService;
    }

    public async Task<AuthenticatedResponse> AuthenticateAsync(AuthenticateRequest model, CancellationToken cancellationToken)
    {
        var user = (await this.userRepository.FindAsync(x => x.Email == model.Email, cancellationToken)).FirstOrDefault();

        if (user == null || !BCrypt.Verify(model.Password, user.PasswordHash))
        {
            throw new UnauthorizedException("Invalid credentials, please try again");
        }

        var jwtToken = jwtService.GenerateJwtToken(user);
        var refreshToken = jwtService.GenerateRefreshToken();

        return new AuthenticatedResponse(user, jwtToken, refreshToken.Token);
    }

    public async Task<AuthenticatedResponse> CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken)
    {
        var user = (await this.userRepository.FindAsync(x => (x.Email == model.Email || x.Email == model.Email), cancellationToken)).FirstOrDefault();

        if (user == null)
        {
            await this.userRepository.AddAsync(new User
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = BCrypt.HashPassword(model.Password),
            }, cancellationToken);

            return await AuthenticateAsync(new AuthenticateRequest
            {
                Email = model.Email,
                Password = model.Password
            }, cancellationToken);
        }

        throw new AppException("email already exists");
    }

    public async Task<User> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var user = await this.userRepository.GetByIdAsync(id, cancellationToken);
        return user ?? throw new KeyNotFoundException("User not found");
    }
}
