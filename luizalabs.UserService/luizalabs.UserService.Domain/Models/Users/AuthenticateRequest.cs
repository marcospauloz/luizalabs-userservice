namespace luizalabs.UserService.Domain.Models.Users;

public class AuthenticateRequest
{
    public string Email { get; set; }

    public string Password { get; set; }
}