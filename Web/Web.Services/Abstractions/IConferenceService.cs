using Web.Services.DTOs;

namespace Web.Services.Abstractions;

public interface IConferenceService
{
    Task<List<ConferenceDto>> GetAllAsync();
    Task<List<ConferenceDto>> GetActiveAsync();
    Task<ConferenceDto?> GetByIdAsync(int id);
    Task<(byte[] Content, string FileName)?> GenerateProgramPdfAsync(int id);
    Task<ConferenceDto> CreateAsync(ConferenceCreateDto dto);
    Task<ConferenceDto?> UpdateAsync(int id, ConferenceUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
