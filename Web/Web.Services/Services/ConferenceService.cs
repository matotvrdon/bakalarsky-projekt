using AutoMapper;
using Web.Domain.Abstractions;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs.Conference;

namespace Web.Services.Services;

public class ConferenceService : IConferenceService
{
    private readonly IConferenceRepository _repository;
    private readonly IMapper _mapper;
    
    public ConferenceService(IConferenceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<List<ConferenceDto>> GetAllAsync()
    {
        return _mapper.Map<List<ConferenceDto>>(await _repository.GetAllAsync());
    }

    public async Task<ConferenceDto?> GetByIdAsync(int id)
    {
        return _mapper.Map<ConferenceDto?>(await _repository.GetByIdAsync(id));
    }

    public async Task<ConferenceDto> AddAsync(CreateConferenceDto createConferenceDto)
    {
        var entity = _mapper.Map<Conference>(createConferenceDto);
        await _repository.AddAsync(entity);
        return _mapper.Map<ConferenceDto>(entity);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }
}