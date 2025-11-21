using Web.Services.DTOs.Day;

namespace Web.Services.Abstractions;

public interface IDayService
{
    Task<List<DayDto>> GetAllByConferenceIdAsync(int conferenceId);
    Task<DayDto?> GetByDayIdAsync(int dayId);
    Task<DayDto> AddAsync(CreateDayDto createDayDto);
    Task<DayDto?> UpdateAsync(UpdateDayDto dayDto);
    Task<bool> DeleteAsync(int dayId);
}