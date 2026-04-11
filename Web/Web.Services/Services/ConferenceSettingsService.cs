using AutoMapper;
using Web.DataAccess.Abstractions;
using Web.Domain.Enums;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Services.Services;

public class ConferenceSettingsService : IConferenceSettingsService
{
    private readonly IConferenceSettingsRepository _conferenceSettingsRepository;
    private readonly IMapper _mapper;

    public ConferenceSettingsService(IConferenceSettingsRepository conferenceSettingsRepository, IMapper mapper)
    {
        _conferenceSettingsRepository = conferenceSettingsRepository;
        _mapper = mapper;
    }

    public async Task<ConferenceSettingsDto?> CreateAsync(int conferenceId, ConferenceSettingsCreateDto dto)
    {
        var conference = await _conferenceSettingsRepository.GetConferenceWithSettingsAsync(conferenceId);
        if (conference == null)
            return null;

        conference.Settings ??= new ConferenceSettings
        {
            ConferenceId = conference.Id,
            Conference = conference,
            ImportantDates = new List<ImportantDates>()
        };
        conference.Settings.ImportantDates ??= new List<ImportantDates>();

        var mappedImportantDates = _mapper.Map<List<ImportantDates>>(dto.ImportantDates ?? new List<ImportantDatesUpdateDto>());

        foreach (var importantDate in mappedImportantDates)
        {
            importantDate.UpdatedDate = null;
            importantDate.ImportantDatesStatus = ImportantDatesStatus.Normal;
            importantDate.ConferenceSettings = conference.Settings;
            conference.Settings.ImportantDates.Add(importantDate);
        }

        await _conferenceSettingsRepository.UpdateConferenceSettingsAsync(conference);

        return MapConferenceSettings(conference);
    }

    public async Task<ImportantDatesDto?> UpdateAsync(int conferenceId, int importantDateId, ImportantDatesUpdatedDateDto dto)
    {
        var conference = await _conferenceSettingsRepository.GetConferenceWithSettingsAsync(conferenceId);
        if (conference == null)
            return null;

        conference.Settings ??= new ConferenceSettings();
        conference.Settings.ImportantDates ??= new List<ImportantDates>();

        var importantDate = conference.Settings.ImportantDates
            .FirstOrDefault(date => date.Id == importantDateId);
        if (importantDate == null)
            return null;

        if (!string.IsNullOrWhiteSpace(dto.Label))
        {
            importantDate.Label = dto.Label.Trim();
        }

        importantDate.UpdatedDate = dto.UpdatedDate;
        importantDate.ImportantDatesStatus = ResolveStatus(importantDate.NormalDate, importantDate.UpdatedDate);

        await _conferenceSettingsRepository.UpdateConferenceSettingsAsync(conference);

        return _mapper.Map<ImportantDatesDto>(importantDate);
    }

    public async Task<bool> DeleteAsync(int conferenceId, int importantDateId)
    {
        var conference = await _conferenceSettingsRepository.GetConferenceWithSettingsAsync(conferenceId);
        if (conference?.Settings?.ImportantDates == null)
            return false;

        var importantDate = conference.Settings.ImportantDates
            .FirstOrDefault(date => date.Id == importantDateId);
        if (importantDate == null)
            return false;

        conference.Settings.ImportantDates.Remove(importantDate);
        await _conferenceSettingsRepository.UpdateConferenceSettingsAsync(conference);

        return true;
    }

    public async Task<ConferenceSettingsDto?> CreateConferenceEntriesAsync(int conferenceId, ConferenceEntryCreateRequestDto dto)
    {
        var conference = await _conferenceSettingsRepository.GetConferenceWithSettingsAsync(conferenceId);
        if (conference == null)
            return null;

        conference.Settings ??= new ConferenceSettings
        {
            ConferenceId = conference.Id,
            Conference = conference
        };
        conference.Settings.ConferenceEntries ??= new List<ConferenceEntry>();

        var mappedConferenceEntries = _mapper.Map<List<ConferenceEntry>>(dto.ConferenceEntries ?? []);

        foreach (var conferenceEntry in mappedConferenceEntries)
        {
            conferenceEntry.ConferenceSettings = conference.Settings;
            conference.Settings.ConferenceEntries.Add(conferenceEntry);
        }

        await _conferenceSettingsRepository.UpdateConferenceSettingsAsync(conference);

        return MapConferenceSettings(conference);
    }

    public async Task<ConferenceEntryDto?> UpdateConferenceEntryAsync(int conferenceId, int conferenceEntryId, ConferenceEntryUpdateDto dto)
    {
        var conference = await _conferenceSettingsRepository.GetConferenceWithSettingsAsync(conferenceId);
        if (conference?.Settings?.ConferenceEntries == null)
            return null;

        var conferenceEntry = conference.Settings.ConferenceEntries.FirstOrDefault(entry => entry.Id == conferenceEntryId);
        if (conferenceEntry == null)
            return null;

        conferenceEntry.Name = dto.Name.Trim();
        conferenceEntry.Price = dto.Price;

        await _conferenceSettingsRepository.UpdateConferenceSettingsAsync(conference);

        return _mapper.Map<ConferenceEntryDto>(conferenceEntry);
    }

    public async Task<bool> DeleteConferenceEntryAsync(int conferenceId, int conferenceEntryId)
    {
        var conference = await _conferenceSettingsRepository.GetConferenceWithSettingsAsync(conferenceId);
        if (conference?.Settings?.ConferenceEntries == null)
            return false;

        var conferenceEntry = conference.Settings.ConferenceEntries.FirstOrDefault(entry => entry.Id == conferenceEntryId);
        if (conferenceEntry == null)
            return false;

        conference.Settings.ConferenceEntries.Remove(conferenceEntry);
        await _conferenceSettingsRepository.UpdateConferenceSettingsAsync(conference);

        return true;
    }

    public async Task<ConferenceSettingsDto?> CreateFoodOptionsAsync(int conferenceId, FoodOptionsCreateRequestDto dto)
    {
        var conference = await _conferenceSettingsRepository.GetConferenceWithSettingsAsync(conferenceId);
        if (conference == null)
            return null;

        conference.Settings ??= new ConferenceSettings
        {
            ConferenceId = conference.Id,
            Conference = conference,
            ImportantDates = new List<ImportantDates>(),
            FoodOptions = new List<FoodOptions>(),
            BookingOptions = new List<BookingOptions>()
        };
        conference.Settings.FoodOptions ??= new List<FoodOptions>();

        var mappedFoodOptions = _mapper.Map<List<FoodOptions>>(dto.FoodOptions ?? new List<FoodOptionsCreateDto>());

        foreach (var foodOption in mappedFoodOptions)
        {
            foodOption.ConferenceSettings = conference.Settings;
            conference.Settings.FoodOptions.Add(foodOption);
        }

        await _conferenceSettingsRepository.UpdateConferenceSettingsAsync(conference);

        return MapConferenceSettings(conference);
    }

    public async Task<FoodOptionsDto?> UpdateFoodOptionAsync(int conferenceId, int foodOptionId, FoodOptionsUpdateDto dto)
    {
        var conference = await _conferenceSettingsRepository.GetConferenceWithSettingsAsync(conferenceId);
        if (conference?.Settings?.FoodOptions == null)
            return null;

        var foodOption = conference.Settings.FoodOptions.FirstOrDefault(option => option.Id == foodOptionId);
        if (foodOption == null)
            return null;

        foodOption.Name = dto.Name.Trim();
        foodOption.Description = dto.Description.Trim();
        foodOption.Date = dto.Date;
        foodOption.Price = dto.Price;
        foodOption.FoodOptionsType = dto.FoodOptionsType;

        await _conferenceSettingsRepository.UpdateConferenceSettingsAsync(conference);

        return _mapper.Map<FoodOptionsDto>(foodOption);
    }

    public async Task<bool> DeleteFoodOptionAsync(int conferenceId, int foodOptionId)
    {
        var conference = await _conferenceSettingsRepository.GetConferenceWithSettingsAsync(conferenceId);
        if (conference?.Settings?.FoodOptions == null)
            return false;

        var foodOption = conference.Settings.FoodOptions.FirstOrDefault(option => option.Id == foodOptionId);
        if (foodOption == null)
            return false;

        conference.Settings.FoodOptions.Remove(foodOption);
        await _conferenceSettingsRepository.UpdateConferenceSettingsAsync(conference);

        return true;
    }

    public async Task<ConferenceSettingsDto?> CreateBookingOptionsAsync(int conferenceId, BookingOptionsCreateRequestDto dto)
    {
        var conference = await _conferenceSettingsRepository.GetConferenceWithSettingsAsync(conferenceId);
        if (conference == null)
            return null;

        conference.Settings ??= new ConferenceSettings
        {
            ConferenceId = conference.Id,
            Conference = conference,
            ImportantDates = new List<ImportantDates>(),
            FoodOptions = new List<FoodOptions>(),
            BookingOptions = new List<BookingOptions>()
        };
        conference.Settings.BookingOptions ??= new List<BookingOptions>();

        var mappedBookingOptions = _mapper.Map<List<BookingOptions>>(dto.BookingOptions ?? new List<BookingOptionsCreateDto>());

        foreach (var bookingOption in mappedBookingOptions)
        {
            bookingOption.ConferenceSettings = conference.Settings;
            conference.Settings.BookingOptions.Add(bookingOption);
        }

        await _conferenceSettingsRepository.UpdateConferenceSettingsAsync(conference);

        return MapConferenceSettings(conference);
    }

    public async Task<BookingOptionsDto?> UpdateBookingOptionAsync(int conferenceId, int bookingOptionId, BookingOptionsUpdateDto dto)
    {
        var conference = await _conferenceSettingsRepository.GetConferenceWithSettingsAsync(conferenceId);
        if (conference?.Settings?.BookingOptions == null)
            return null;

        var bookingOption = conference.Settings.BookingOptions.FirstOrDefault(option => option.Id == bookingOptionId);
        if (bookingOption == null)
            return null;

        bookingOption.Name = dto.Name.Trim();
        bookingOption.Description = dto.Description.Trim();
        bookingOption.StartDate = dto.StartDate;
        bookingOption.EndDate = dto.EndDate;
        bookingOption.Price = dto.Price;

        await _conferenceSettingsRepository.UpdateConferenceSettingsAsync(conference);

        return _mapper.Map<BookingOptionsDto>(bookingOption);
    }

    public async Task<bool> DeleteBookingOptionAsync(int conferenceId, int bookingOptionId)
    {
        var conference = await _conferenceSettingsRepository.GetConferenceWithSettingsAsync(conferenceId);
        if (conference?.Settings?.BookingOptions == null)
            return false;

        var bookingOption = conference.Settings.BookingOptions.FirstOrDefault(option => option.Id == bookingOptionId);
        if (bookingOption == null)
            return false;

        conference.Settings.BookingOptions.Remove(bookingOption);
        await _conferenceSettingsRepository.UpdateConferenceSettingsAsync(conference);

        return true;
    }

    public async Task<ConferenceSettingsDto?> ReplaceProgramAsync(int conferenceId, ProgramReplaceRequestDto dto)
    {
        var conference = await _conferenceSettingsRepository.GetConferenceWithSettingsAsync(conferenceId);
        if (conference == null)
            return null;

        conference.Settings ??= new ConferenceSettings
        {
            ConferenceId = conference.Id,
            Conference = conference
        };
        conference.Settings.ProgramDays ??= new List<ProgramDay>();
        conference.Settings.ProgramDays.Clear();

        foreach (var dayDto in dto.ProgramDays.OrderBy(day => day.Order))
        {
            var programDay = new ProgramDay
            {
                Label = dayDto.Label.Trim(),
                Date = dayDto.Date,
                Order = dayDto.Order,
                ConferenceSettings = conference.Settings
            };

            foreach (var itemDto in dayDto.ProgramItems.OrderBy(item => item.Order))
            {
                var programItem = new ProgramItem
                {
                    Title = itemDto.Title.Trim(),
                    StartTime = itemDto.StartTime,
                    EndTime = itemDto.EndTime,
                    Location = string.IsNullOrWhiteSpace(itemDto.Location) ? null : itemDto.Location.Trim(),
                    Speaker = string.IsNullOrWhiteSpace(itemDto.Speaker) ? null : itemDto.Speaker.Trim(),
                    Chair = string.IsNullOrWhiteSpace(itemDto.Chair) ? null : itemDto.Chair.Trim(),
                    Type = itemDto.Type,
                    Order = itemDto.Order,
                    ProgramDay = programDay
                };

                foreach (var sessionDto in itemDto.Sessions.OrderBy(session => session.Order))
                {
                    var programSession = new ProgramSession
                    {
                        SessionName = sessionDto.SessionName.Trim(),
                        StartTime = sessionDto.StartTime,
                        EndTime = sessionDto.EndTime,
                        Chair = string.IsNullOrWhiteSpace(sessionDto.Chair) ? null : sessionDto.Chair.Trim(),
                        Order = sessionDto.Order,
                        ProgramItem = programItem
                    };

                    foreach (var presentationDto in sessionDto.Presentations.OrderBy(presentation => presentation.Order))
                    {
                        programSession.ProgramPresentations.Add(new ProgramPresentation
                        {
                            StartTime = presentationDto.StartTime,
                            EndTime = presentationDto.EndTime,
                            Authors = presentationDto.Authors.Trim(),
                            Title = presentationDto.Title.Trim(),
                            Order = presentationDto.Order,
                            ProgramSession = programSession
                        });
                    }

                    programItem.ProgramSessions.Add(programSession);
                }

                programDay.ProgramItems.Add(programItem);
            }

            conference.Settings.ProgramDays.Add(programDay);
        }

        await _conferenceSettingsRepository.UpdateConferenceSettingsAsync(conference);

        return MapConferenceSettings(conference);
    }

    private static ImportantDatesStatus ResolveStatus(DateOnly normalDate, DateOnly? updatedDate)
    {
        if (!updatedDate.HasValue || updatedDate.Value == normalDate)
            return ImportantDatesStatus.Normal;

        return updatedDate.Value > normalDate
            ? ImportantDatesStatus.Extended
            : ImportantDatesStatus.Shortened;
        
    }

    private ConferenceSettingsDto MapConferenceSettings(Conference conference)
    {
        return conference.Settings != null
            ? _mapper.Map<ConferenceSettingsDto>(conference.Settings)
            : new ConferenceSettingsDto();
    }
}
