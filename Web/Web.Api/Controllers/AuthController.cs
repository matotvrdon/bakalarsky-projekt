using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Data;
using Web.Domain.Enums;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly AppDbContext _appDbContext;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthController(IAuthService authService, AppDbContext appDbContext, IPasswordHasher<User> passwordHasher)
    {
        _authService = authService;
        _appDbContext = appDbContext;
        _passwordHasher = passwordHasher;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var response = await _authService.LoginAsync(dto);
        if (response is null)
            return Unauthorized();
        return Ok(response);
    }



    [HttpPost("register-simple")]
    public async Task<IActionResult> Register([FromBody] RegistrationSimpleRequestDto dto)
    {
        var response = await _authService.RegisterAsync(dto);
        if (response.MessageStatus == MessageStatus.Error)
            return Conflict(response);
        return Ok(response);
    }
}
