using Web.Services.DTOs;

namespace Web.Services.Abstractions;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto);
    Task<LoginResponseDto?> AdminLoginAsync(LoginRequestDto dto);
    Task<RegistrationSimpleResponseDto> RegisterAsync(RegistrationSimpleRequestDto dto);
    Task<RegistrationBasicResponseDto> RegisterBasicAsync(RegistrationBasicRequestDto dto);
    Task<RegistrationAccountResponseDto> RegisterAccountAsync(RegistrationAccountRequestDto dto);
}