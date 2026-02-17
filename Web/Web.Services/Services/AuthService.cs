using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Web.Domain.Abstractions;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Services.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IMapper _mapper;

    public AuthService(IAuthRepository authRepository, IPasswordHasher<User> passwordHasher, IMapper mapper)
    {
        _authRepository = authRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
    {
        var email = dto.Email.Trim();
        var user = await _authRepository.GetByEmailAsync(email);
        if (user is null)
            return null;
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (result == PasswordVerificationResult.Failed)
            return null;

        return new LoginResponseDto
        {
            User = _mapper.Map<UserDto>(user)
        };
    }
}
