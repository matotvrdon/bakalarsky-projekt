using AutoMapper;
using Web.Domain.Abstractions;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs.Talk;

namespace Web.Services.Services;

public class TalkService : ITalkService
{
    private readonly ITalkRepository _talkRepository;
    private readonly IMapper _mapper;

    public TalkService(ITalkRepository talkRepository, IMapper mapper)
    {
        _talkRepository = talkRepository;
        _mapper = mapper;
    }

    public async Task<List<TalkDto>> GetAllByThemeIdAsync(int themeId)
    {
        var talks = await _talkRepository.GetAllByThemeIdAsync(themeId);
        return _mapper.Map<List<TalkDto>>(talks);
    }

    public async Task<TalkDto?> GetByTalkIdAsync(int talkId)
    {
        var talk = await _talkRepository.GetByTalkIdAsync(talkId);
        return _mapper.Map<TalkDto?>(talk);
    }

    public async Task<TalkDto> AddAsync(CreateTalkDto createTalkDto)
    {
        var talk = _mapper.Map<Talk>(createTalkDto);
        await _talkRepository.AddAsync(talk);
        return _mapper.Map<TalkDto>(talk);
    }

    public async Task<TalkDto?> UpdateAsync(UpdateTalkDto updateTalkDto)
    {
        var talk = await _talkRepository.GetByTalkIdAsync(updateTalkDto.Id);
        
        if (talk == null)
        {
            return null;
        }
        
        talk.Title = updateTalkDto.Title;
        talk.Content = updateTalkDto.Content;
        talk.StartTime = updateTalkDto.StartTime;
        talk.EndTime = updateTalkDto.EndTime;
        
        await _talkRepository.UpdateAsync(talk);
        
        return _mapper.Map<TalkDto?>(talk);
    }

    public async Task<bool> DeleteAsync(int talkId)
    {
        return await _talkRepository.DeleteAsync(talkId);
    }
}
