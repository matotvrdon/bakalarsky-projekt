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
    private readonly IProgramPdfGenerator _programPdfGenerator;

    public ConferenceService(
        IConferenceRepository conferenceRepository,
        IMapper mapper,
        IProgramPdfGenerator programPdfGenerator)
    {
        _conferenceRepository = conferenceRepository;
        _mapper = mapper;
        _programPdfGenerator = programPdfGenerator;
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

    public async Task<(byte[] Content, string FileName)?> GenerateProgramPdfAsync(int id)
    {
        var conferenceDto = await GetByIdAsync(id);

        if (conferenceDto?.Settings == null)
        {
            return null;
        }

        var fileNameBase = CreateSafeFileName(conferenceDto.Name);

        var pdfBytes = _programPdfGenerator.GenerateProgramPdf(conferenceDto);

        return (pdfBytes, $"{fileNameBase}-program.pdf");
    }

    private static string CreateSafeFileName(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "program-konferencie";
        }

        var invalidChars = Path.GetInvalidFileNameChars();

        var cleaned = new string(
            value
                .Trim()
                .Select(character => invalidChars.Contains(character) ? '-' : character)
                .ToArray()
        );

        return cleaned.Replace(" ", "-");
    }

    public async Task<ConferenceDto> CreateAsync(ConferenceCreateDto dto)
    {
        var conference = _mapper.Map<Conference>(dto);
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

}
