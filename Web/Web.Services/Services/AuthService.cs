using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Cryptography;
using Web.DataAccess.Abstractions;
using Web.DataAccess.Data;
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
    private readonly IConferenceRepository _conferenceRepository;
    private readonly AppDbContext _dbContext;
    private readonly IEmailService _emailService;

    public AuthService(
        IAuthRepository authRepository,
        IPasswordHasher<User> passwordHasher,
        IMapper mapper,
        IParticipantRepository participantRepository,
        IConferenceRepository conferenceRepository,
        AppDbContext dbContext,
        IEmailService emailService)
    {
        _authRepository = authRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _participantRepository = participantRepository;
        _conferenceRepository = conferenceRepository;
        _dbContext = dbContext;
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

        if (dto.ParticipantId.HasValue)
        {
            await LinkParticipantToExistingUserAsync(user, dto.ParticipantId.Value);
        }

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
        var email = NormalizeEmail(dto.Email);
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
    
    public async Task<RegistrationBasicResponseDto> RegisterBasicAsync(RegistrationBasicRequestDto dto)
    {
        var conferenceExists = await _conferenceRepository.ExistsAsync(dto.ConferenceId);
        if (!conferenceExists)
        {
            throw new RegistrationFlowException(HttpStatusCode.NotFound, new RegistrationErrorResponseDto
            {
                Code = "CONFERENCE_NOT_FOUND",
                Message = $"Conference with id {dto.ConferenceId} was not found."
            });
        }

        var participant = _mapper.Map<Participant>(dto);
        participant.FirstName = participant.FirstName.Trim();
        participant.LastName = participant.LastName.Trim();
        participant.Phone = NormalizeOptional(dto.Phone);
        participant.Affiliation = NormalizeOptional(dto.Affiliation);
        participant.Country = NormalizeOptional(dto.Country);
        participant.UserId = null;

        await _participantRepository.AddAsync(participant);

        return new RegistrationBasicResponseDto
        {
            ParticipantId = participant.Id,
            Status = "basic_created",
            Message = "Basic registration created."
        };
    }

    public async Task<RegistrationAccountResponseDto> RegisterAccountAsync(RegistrationAccountRequestDto dto)
    {
        var participant = await _participantRepository.GetByIdAsync(dto.ParticipantId);
        if (participant == null)
        {
            throw new RegistrationFlowException(HttpStatusCode.NotFound, new RegistrationErrorResponseDto
            {
                Code = "PARTICIPANT_NOT_FOUND",
                Message = $"Participant with id {dto.ParticipantId} was not found."
            });
        }

        if (participant.UserId.HasValue)
        {
            throw new RegistrationFlowException(HttpStatusCode.Conflict, new RegistrationErrorResponseDto
            {
                Code = "REGISTRATION_ALREADY_COMPLETED",
                Message = "Participant already has a linked user account."
            });
        }

        var email = NormalizeEmail(dto.Email);
        if (await _authRepository.ExistsAsync(email))
        {
            throw new RegistrationFlowException(HttpStatusCode.Conflict, new RegistrationErrorResponseDto
            {
                Code = "EMAIL_EXISTS",
                Message = "Email already exists. Prihláste sa existujúcim účtom.",
                Field = "email"
            });
        }

        var user = new User
        {
            Email = email,
            PasswordHash = string.Empty,
            Role = UserRole.Participant,
            CreatedAt = DateTime.UtcNow
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await _authRepository.AddAsync(user);
            participant.UserId = user.Id;
            await _participantRepository.UpdateAsync(participant);
            await transaction.CommitAsync();
        }
        catch (DbUpdateException)
        {
            await transaction.RollbackAsync();
            throw new RegistrationFlowException(HttpStatusCode.Conflict, new RegistrationErrorResponseDto
            {
                Code = "EMAIL_EXISTS",
                Message = "Email already exists.",
                Field = "email"
            });
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return new RegistrationAccountResponseDto
        {
            ParticipantId = participant.Id,
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            },
            Message = "Account created."
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

    private static string NormalizeEmail(string email)
    {
        return email.Trim().ToLowerInvariant();
    }

    private static string? NormalizeOptional(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;
        return value.Trim();
    }

    private async Task LinkParticipantToExistingUserAsync(User user, int participantId)
    {
        var participant = await _participantRepository.GetByIdAsync(participantId);
        if (participant == null)
        {
            throw new RegistrationFlowException(HttpStatusCode.NotFound, new RegistrationErrorResponseDto
            {
                Code = "PARTICIPANT_NOT_FOUND",
                Message = $"Participant with id {participantId} was not found."
            });
        }

        if (participant.UserId == user.Id)
        {
            return;
        }

        var existingParticipant = await _participantRepository.GetByUserIdConferenceIdAsync(user.Id, participant.ConferenceId);
        if (existingParticipant != null && existingParticipant.Id != participant.Id)
        {
            if (!participant.UserId.HasValue)
            {
                await _participantRepository.DeleteAsync(participant);
            }
            return;
        }

        if (participant.UserId.HasValue && participant.UserId != user.Id)
        {
            throw new RegistrationFlowException(HttpStatusCode.Conflict, new RegistrationErrorResponseDto
            {
                Code = "PARTICIPANT_ALREADY_LINKED",
                Message = "Participant already has a linked user account."
            });
        }

        participant.UserId = user.Id;
        await _participantRepository.UpdateAsync(participant);
    }
}
