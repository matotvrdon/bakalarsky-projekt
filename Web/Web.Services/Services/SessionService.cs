using AutoMapper;
using Web.Domain.Abstractions;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs.Session;

namespace Web.Services.Services;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IMapper _mapper;

    public SessionService(ISessionRepository sessionRepository, IMapper mapper)
    {
        _sessionRepository = sessionRepository;
        _mapper = mapper;
    }

    public async Task<List<SessionDto>> GetAllByDayIdAsync(int dayId)
    {
        var sessions = await _sessionRepository.GetAllByDayIdAsync(dayId);
        return _mapper.Map<List<SessionDto>>(sessions);
    }

    public async Task<SessionDto?> GetBySessionIdAsync(int sessionId)
    {
        var session = await _sessionRepository.GetBySessionIdAsync(sessionId);
        return _mapper.Map<SessionDto?>(session);
    }

    public async Task<SessionDto> AddAsync(CreateSessionDto createSessionDto)
    {
        var session = _mapper.Map<Session>(createSessionDto);
        await _sessionRepository.AddAsync(session);
        return _mapper.Map<SessionDto>(session);
    }
}