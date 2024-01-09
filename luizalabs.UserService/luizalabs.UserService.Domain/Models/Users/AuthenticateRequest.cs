namespace luizalabs.UserService.Domain.Models.Users;

using System.ComponentModel.DataAnnotations;

public class AuthenticateRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}