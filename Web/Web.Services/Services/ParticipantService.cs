using AutoMapper;
using Web.DataAccess.Abstractions;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Services.Services;

public class ParticipantService : IParticipantService
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IMapper _mapper;
    
    public ParticipantService(IParticipantRepository participantRepository, IMapper mapper)
    {
        _participantRepository = participantRepository;
        _mapper = mapper;
    }

    public async Task<ParticipantDto?> GetByUserIdAsync(int userId)
    {
        var participant = await _participantRepository.GetByUserIdAsync(userId);
        return participant == null ? null : _mapper.Map<ParticipantDto>(participant);
    }

    public async Task<ParticipantDto?> UpdateAsync(ParticipantUpdateDto dto)
    {
        var participant = await _participantRepository.GetByUserIdConferenceIdAsync(dto.UserId, dto.ConferenceId);
        if (participant == null)
            return null;
        
        _mapper.Map(dto, participant);
        await _participantRepository.UpdateAsync(participant);
        
        return _mapper.Map<ParticipantDto>(participant);
    }

    public async Task<List<ParticipantDto>> GetAllByActiveConferenceAsync()
    {
        var participants = await _participantRepository.GetAllByActiveConferenceAsync();
        if (participants == null)
            throw new KeyNotFoundException(nameof(participants));
        return _mapper.Map<List<ParticipantDto>>(participants);
    }
}