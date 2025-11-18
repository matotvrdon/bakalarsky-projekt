using Web.Domain.Models;

namespace Web.Domain.Abstractions;

public interface IAttendeeRepository
{
    Task<List<Attendee>> GetAllByCustomerIdAsync(int customerId);
    Task<List<Attendee?>> GetAllByAttendeeIdAsync(List<int> attendeeId);
    Task AddAsync(Attendee attendee);
    Task UpdateAsync(Attendee attendee);
    Task<Attendee?> GetAttendeeByIdAsync(int id);
}