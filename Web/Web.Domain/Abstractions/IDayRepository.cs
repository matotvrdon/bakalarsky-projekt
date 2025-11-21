using Web.Domain.Models;

namespace Web.Domain.Abstractions;

public interface IDayRepository
{
    Task<List<Day>> GetAllByConferenceIdAsync(int conferenceId);
    Task<Day?> GetByDayIdAsync(int dayId);
    Task AddAsync(Day day);
    Task UpdateAsync(Day day);
    Task<bool> DeleteAsync(int dayId);
}