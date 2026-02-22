using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using Web.DataAccess.Abstractions;
using Web.Domain.Enums;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs;
using Web.Services.Exceptions;

namespace Web.Services.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IMapper _mapper;
    private readonly IParticipantRepository _participantRepository;
    private readonly IEmailService _emailService;

    public AuthService(
        IAuthRepository authRepository,
        IPasswordHasher<User> passwordHasher,
        IMapper mapper,
        IParticipantRepository participantRepository,
        IEmailService emailService)
    {
        _authRepository = authRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _participantRepository = participantRepository;
        _emailService = emailService;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
    {
        var email = dto.Email.Trim().ToLower();
        var user = await _authRepository.GetByEmailAsync(email);
        if (user is null)
            return null;
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (result == PasswordVerificationResult.Failed)
            return null;

        return new LoginResponseDto
        {
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            }
        };
    }

    public async Task<RegistrationSimpleResponseDto> RegisterAsync(RegistrationSimpleRequestDto dto)
    {
        var email = dto.Email.Trim().ToLower();
        var (user, password) = await GenerateUser(email);
        var participant = _mapper.Map<Participant>(dto);
        _mapper.Map(user, participant);
        if (await _participantRepository.ExistsAsync(email, participant.ConferenceId))
        {
            var response = new RegistrationSimpleResponseDto
            {
                Message = $"Email {email} už existuje na konferenciu {participant.ConferenceId}",
                Email = email,
                
            };
            
            throw new RegistrationConflictException(response);
        }
        await _participantRepository.AddAsync(participant);
        if (!string.IsNullOrWhiteSpace(password))
        {
            await _emailService.SendEmailCredentialsAsync(user.Email, password);
        }
        return new RegistrationSimpleResponseDto
        {
            Message = string.IsNullOrWhiteSpace(password)
                ? "Registrácia bola úspešná. Účet už existuje, prihláste sa existujúcimi údajmi."
                : "Prihlasovacie údaje boli odoslané na email uvedený v registrácii",
            Email = user.Email,
        };
    }
    
    
    private async Task<(User user, string? password)> GenerateUser(string email)
    {
        if (await _authRepository.ExistsAsync(email))
        {
            var existingUser = await _authRepository.GetByEmailAsync(email);
            if (existingUser is null)
                throw new InvalidOperationException("User exists but could not be loaded.");
            return (existingUser, null);
        }

        var user = new User
        {
            Email = email,
            PasswordHash = "",
            Role = UserRole.Participant,
            CreatedAt = DateTime.UtcNow
        };
        var password = GeneratePassword();
        user.PasswordHash = _passwordHasher.HashPassword(user, password);
        
        await _authRepository.AddAsync(user);
        return (user, password);
    }

    private string GeneratePassword()
    {
        const int length = 8;
        const string symbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        var password = new char[length];
        for (var i = 0; i < length; i++)
        {
            password[i] = symbols[RandomNumberGenerator.GetInt32(symbols.Length)];
        }

        return new string(password);
    }
}
