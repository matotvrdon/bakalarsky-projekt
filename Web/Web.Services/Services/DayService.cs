using AutoMapper;
using Web.Domain.Abstractions;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs.Day;

namespace Web.Services.Services;

public class DayService : IDayService
{
    private readonly IDayRepository _dayRepository;
    private readonly IMapper _mapper;

    public DayService(IDayRepository dayRepository, IMapper mapper)
    {
        _dayRepository = dayRepository;
        _mapper = mapper;
    }

    public async Task<List<DayDto>> GetAllByConferenceIdAsync(int conferenceId)
    {
        var day = await _dayRepository.GetAllByConferenceIdAsync(conferenceId);
        return _mapper.Map<List<DayDto>>(day);
    }

    public async Task<DayDto?> GetByDayIdAsync(int dayId)
    {
        var day = await _dayRepository.GetByDayIdAsync(dayId);
        return _mapper.Map<DayDto?>(day);
    }

    public async Task<DayDto> AddAsync(CreateDayDto createDayDto)
    {
        var day = _mapper.Map<Day>(createDayDto);
        await _dayRepository.AddAsync(day);
        return _mapper.Map<DayDto>(day);
    }
}