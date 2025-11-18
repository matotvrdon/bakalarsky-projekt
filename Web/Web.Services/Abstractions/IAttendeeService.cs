using Web.Services.DTOs.Attendee;

namespace Web.Services.Abstractions;

public interface IAttendeeService
{
    Task<List<AttendeeDto>> GetAllAttendeesByCustomerIdAsync(int customerId);
    Task<AttendeeDto> AddAttendeeAsync(CreateAttendeeDto createAttendeeDto);
    Task<List<AttendeeDto?>> UpdateAttendeeAsync(UpdateAttendeeDto updateAttendeeDto);
    Task<AttendeeDto?> GetAttendeeByIdAsync(int id);
}