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
        var conferenceDtos = _mapper.Map<List<ConferenceDto>>(conferences);

        await SetParticipantCountsAsync(conferenceDtos);

        return conferenceDtos;
    }

    public async Task<List<ConferenceDto>> GetActiveAsync()
    {
        var conferences = await _conferenceRepository.GetActiveAsync();
        var conferenceDtos = _mapper.Map<List<ConferenceDto>>(conferences);

        await SetParticipantCountsAsync(conferenceDtos);

        return conferenceDtos;
    }

    public async Task<ConferenceDto?> GetByIdAsync(int id)
    {
        var conference = await _conferenceRepository.GetByIdAsync(id);

        if (conference == null)
        {
            return null;
        }

        var conferenceDto = _mapper.Map<ConferenceDto>(conference);

        await SetParticipantCountsAsync(new List<ConferenceDto>
        {
            conferenceDto
        });

        return conferenceDto;
    }

    public async Task<ConferenceDto?> GetPublicByIdAsync(int id)
    {
        var conference = await _conferenceRepository.GetPublicByIdAsync(id);

        if (conference == null)
        {
            return null;
        }

        var conferenceDto = _mapper.Map<ConferenceDto>(conference);

        await SetParticipantCountsAsync(new List<ConferenceDto>
        {
            conferenceDto
        });

        return conferenceDto;
    }

    public async Task<ConferenceDto?> GetPreviewByIdAsync(int id)
    {
        var conference = await _conferenceRepository.GetByIdAsync(id);

        if (conference == null)
        {
            return null;
        }

        var conferenceDto = _mapper.Map<ConferenceDto>(conference);

        await SetParticipantCountsAsync(new List<ConferenceDto>
        {
            conferenceDto
        });

        return conferenceDto;
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

    public async Task<ConferenceDto> CreateAsync(ConferenceCreateDto dto)
    {
        var conference = _mapper.Map<Conference>(dto);

        await _conferenceRepository.AddAsync(conference);

        var conferenceDto = _mapper.Map<ConferenceDto>(conference);
        conferenceDto.ParticipantsCount = 0;

        return conferenceDto;
    }

    public async Task<ConferenceDto?> UpdateAsync(int id, ConferenceUpdateDto dto)
    {
        var conference = await _conferenceRepository.GetByIdAsync(id);

        if (conference == null)
        {
            return null;
        }

        _mapper.Map(dto, conference);

        await _conferenceRepository.UpdateAsync(conference);

        var updatedConference = await _conferenceRepository.GetByIdAsync(id);

        if (updatedConference == null)
        {
            return null;
        }

        var conferenceDto = _mapper.Map<ConferenceDto>(updatedConference);

        await SetParticipantCountsAsync(new List<ConferenceDto>
        {
            conferenceDto
        });

        return conferenceDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var conference = await _conferenceRepository.GetByIdAsync(id);

        if (conference == null)
        {
            return false;
        }

        await _conferenceRepository.DeleteAsync(conference);

        return true;
    }

    private async Task SetParticipantCountsAsync(List<ConferenceDto> conferences)
    {
        var conferenceIds = conferences
            .Select(conference => conference.Id)
            .Distinct()
            .ToList();

        var participantCounts = await _conferenceRepository.GetParticipantCountsAsync(conferenceIds);

        foreach (var conference in conferences)
        {
            conference.ParticipantsCount = participantCounts.TryGetValue(conference.Id, out var count)
                ? count
                : 0;
        }
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
}