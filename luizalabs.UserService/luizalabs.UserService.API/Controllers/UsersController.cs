namespace luizalabs.UserService.API.Controllers;

using Authorization;
using Application.Interface.Service;
using Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    {
        var response = await _userService.AuthenticateAsync(model, HttpContext.RequestAborted);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser(CreateUserRequest model)
    {
        await _userService.CreateUserAsync(model, HttpContext.RequestAborted);
        var response = await _userService.AuthenticateAsync(
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
        var user = await _userService.GetByIdAsync(id, HttpContext.RequestAborted);
        return Ok(user);
    }
}
