using AutoMapper;
using Web.DataAccess.Abstractions;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Services.Services;

public class ConferenceService : IConferenceService
{
    private readonly IConferenceRepository _conferenceRepository;
    private readonly IMapper _mapper;
    
    public ConferenceService(IConferenceRepository conferenceRepository, IMapper mapper)
    {
        _conferenceRepository = conferenceRepository;
        _mapper = mapper;
    }

    public async Task<List<ConferenceDto>> GetAllAsync()
    {
        var conferences = await _conferenceRepository.GetAllAsync();
        return _mapper.Map<List<ConferenceDto>>(conferences);
    }

    public async Task<List<ConferenceDto>> GetActiveAsync()
    {
        var conferences = await _conferenceRepository.GetActiveAsync();
        return _mapper.Map<List<ConferenceDto>>(conferences);
    }

    public async Task<ConferenceDto?> GetByIdAsync(int id)
    {
        var conference = await _conferenceRepository.GetByIdAsync(id);
        
        return conference == null ? null : _mapper.Map<ConferenceDto>(conference);
    }

    public async Task<ConferenceDto> CreateAsync(ConferenceCreateDto dto)
    {
        var conference = _mapper.Map<Conference>(dto);
        conference.StartDate = EnsureUtc(conference.StartDate);
        conference.EndDate = EnsureUtc(conference.EndDate);
        conference.IsActive = true;
        await _conferenceRepository.AddAsync(conference);
        return _mapper.Map<ConferenceDto>(conference);
    }

    public async Task<ConferenceDto?> UpdateAsync(int id, ConferenceUpdateDto dto)
    {
        var conference = await _conferenceRepository.GetByIdAsync(id);
        if (conference == null)
            return null;
        _mapper.Map(dto, conference);
        conference.StartDate = EnsureUtc(conference.StartDate);
        conference.EndDate = EnsureUtc(conference.EndDate);
        await _conferenceRepository.UpdateAsync(conference);
        return _mapper.Map<ConferenceDto>(conference);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var conference = await _conferenceRepository.GetByIdAsync(id);
        if (conference == null)
            return false;
        await _conferenceRepository.DeleteAsync(conference);
        return true;
    }

    private static DateTime EnsureUtc(DateTime value)
    {
        return value.Kind switch
        {
            DateTimeKind.Utc => value,
            DateTimeKind.Local => value.ToUniversalTime(),
            _ => DateTime.SpecifyKind(value, DateTimeKind.Utc)
        };
    }
}
