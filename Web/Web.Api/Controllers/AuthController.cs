using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs;
using Web.Services.Exceptions;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
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
        try
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(result);
        }
        catch (RegistrationConflictException ex)
        {
            return Conflict(ex.Response);
        }
    }

    [HttpPost("register-basic")]
    [ProducesResponseType(typeof(RegistrationBasicResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RegistrationErrorResponseDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RegisterBasic([FromBody] RegistrationBasicRequestDto dto)
    {
        try
        {
            var result = await _authService.RegisterBasicAsync(dto);
            return StatusCode(StatusCodes.Status201Created, result);
        }
        catch (RegistrationFlowException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Response);
        }
    }

    [HttpPost("register-account")]
    [ProducesResponseType(typeof(RegistrationAccountResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RegistrationErrorResponseDto), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RegistrationErrorResponseDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RegisterAccount([FromBody] RegistrationAccountRequestDto dto)
    {
        try
        {
            var result = await _authService.RegisterAccountAsync(dto);
            return Ok(result);
        }
        catch (RegistrationFlowException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Response);
        }
    }
}
