namespace luizalabs.UserService.Application.Interface.Service;

using Domain.Entities;

public interface IJwtService
{
    public string GenerateJwtToken(User user);

    public string? ValidateJwtToken(string? token);

    public RefreshToken GenerateRefreshToken();
}
