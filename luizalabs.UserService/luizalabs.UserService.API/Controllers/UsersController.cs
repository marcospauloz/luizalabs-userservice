namespace luizalabs.UserService.API.Controllers;

using Dotnet.MiniJira.Domain.Models.Users;
using luizalabs.UserService.API.Authorization;
using luizalabs.UserService.Application.Interface.Service;
using luizalabs.UserService.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService userService;

    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    {
        var response = await userService.AuthenticateAsync(model, HttpContext.RequestAborted);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser(CreateUserRequest model)
    {
        await userService.CreateUserAsync(model, HttpContext.RequestAborted);
        var response = await userService.AuthenticateAsync(
            new AuthenticateRequest
            {
                Password = model.Password,
                Email = model.Email
            }, HttpContext.RequestAborted);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var user = await userService.GetByIdAsync(id, HttpContext.RequestAborted);
        return Ok(user);
    }
}
