using AutoMapper;
using Web.Domain.Abstractions;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs.Attendee;

namespace Web.Services.Services;

public class AttendeeService : IAttendeeService
{
    private readonly IAttendeeRepository _attendeeRepository;
    private readonly IMapper _mapper;
    
    public AttendeeService(IAttendeeRepository attendeeRepository, IMapper mapper)
    {
        _attendeeRepository = attendeeRepository;
        _mapper = mapper;
    }

    public async Task<List<AttendeeDto>> GetAllAttendeesByCustomerIdAsync(int customerId)
    {
        return _mapper.Map<List<AttendeeDto>>(await _attendeeRepository.GetAllByCustomerIdAsync(customerId));
    }

    public async Task<AttendeeDto> AddAttendeeAsync(CreateAttendeeDto createAttendeeDto)
    {
        var attendee = _mapper.Map<Attendee>(createAttendeeDto);
        await _attendeeRepository.AddAsync(attendee);
        return _mapper.Map<AttendeeDto>(attendee);
    }

    public async Task<List<AttendeeDto?>> UpdateAttendeeAsync(UpdateAttendeeDto updateAttendeeDto)
    {
        List<Attendee?> attendees = await _attendeeRepository.GetAllByAttendeeIdAsync(updateAttendeeDto.AttendeeId);

        foreach (var attendee in attendees) {
            if (attendee == null)
            {
                continue;
            }
            attendee.CustomerId = updateAttendeeDto.CustomerId;
            await _attendeeRepository.UpdateAsync(attendee);
        }
        return _mapper.Map<List<AttendeeDto?>>(attendees);
    }

    public async Task<AttendeeDto?> GetAttendeeByIdAsync(int id)
    {
        return _mapper.Map<AttendeeDto?>(await _attendeeRepository.GetAttendeeByIdAsync(id));
    }
}
