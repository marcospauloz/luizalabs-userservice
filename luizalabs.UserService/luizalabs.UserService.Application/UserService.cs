namespace luizalabs.UserService.Application;

using BCrypt.Net;
using Interface.Repository;
using Interface.Service;
using Domain.Core.Exceptions;
using Domain.Entities;
using Domain.Models.Users;
using System.Threading.Tasks;
using FluentValidation;

public class UserService : IUserService
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IValidator<CreateUserRequest> _createUserRequestValidator;

    public UserService(
        IBaseRepository<User> userRepository,
        IJwtService jwtService,
        IValidator<CreateUserRequest> createUserRequestValidator)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _createUserRequestValidator = createUserRequestValidator;
    }

    public async Task<AuthenticatedResponse> AuthenticateAsync(AuthenticateRequest model,
        CancellationToken cancellationToken)
    {
        var user = (await _userRepository.FindAsync(x => x.Email == model.Email, cancellationToken)).FirstOrDefault();

        if (user == null || !BCrypt.Verify(model.Password, user.PasswordHash))
        {
            throw new UnauthorizedException("E-mail ou senha inválidos.");
        }

        var jwtToken = _jwtService.GenerateJwtToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        return new AuthenticatedResponse(user, jwtToken, refreshToken.Token);
    }

    public async Task<AuthenticatedResponse> CreateUserAsync(CreateUserRequest model,
        CancellationToken cancellationToken)
    {
        var results = await _createUserRequestValidator.ValidateAsync(model, cancellationToken);

        if (!results.IsValid)
        {
            throw new AppException(string.Join(" ", results.Errors.Select(x => x.ErrorMessage)));
        }

        var user = (await _userRepository.FindAsync(x => (x.Email == model.Email || x.Email == model.Email),
            cancellationToken)).FirstOrDefault();

        if (user != null)
        {
            throw new AppException("E-mail já existe");
        }

        await _userRepository.AddAsync(new User
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

    public async Task<User> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        return user ?? throw new KeyNotFoundException("Usuário não encontrado");
    }
}