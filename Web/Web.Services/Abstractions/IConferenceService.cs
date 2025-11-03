using Web.Services.DTOs.Conference;

namespace Web.Services.Abstractions;

public interface IConferenceService
{
    Task<List<ConferenceDto>> GetAllAsync();
    Task<ConferenceDto?> GetByIdAsync(int id);
    Task<ConferenceDto> AddAsync(CreateConferenceDto createConferenceDto);
    Task<bool> DeleteAsync(int id);
}