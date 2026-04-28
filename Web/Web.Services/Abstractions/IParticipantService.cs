using Web.Domain.Models;
using Web.Services.DTOs;

namespace Web.Services.Abstractions;

public interface IParticipantService
{
    Task<ParticipantDto?> GetByUserIdAsync(int userId);
    Task<ParticipantDto?> UpdateAsync(ParticipantUpdateDto dto);
    Task<List<ParticipantDto>> GetAllByActiveConferenceAsync();
}