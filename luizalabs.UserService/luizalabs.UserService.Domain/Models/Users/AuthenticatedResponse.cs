using luizalabs.UserService.Domain.Entities;
using System.Text.Json.Serialization;

namespace luizalabs.UserService.Domain.Models.Users;

public class AuthenticatedResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string JwtToken { get; set; }
    public string Email { get; set; }

    [JsonIgnore]
    public string RefreshToken { get; set; }

    public AuthenticatedResponse(User user, string jwtToken, string refreshToken)
    {
        Id = user.Id;
        Name = user.Name;
        JwtToken = jwtToken;
        Email = user.Email;
        RefreshToken = refreshToken;
    }
}
