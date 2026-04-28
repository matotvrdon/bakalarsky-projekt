using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/conference/{conferenceId:int}")]
public class ConferenceSettingsController : ControllerBase
{
    private readonly IConferenceSettingsService _conferenceSettingsService;

    public ConferenceSettingsController(IConferenceSettingsService conferenceSettingsService)
    {
        _conferenceSettingsService = conferenceSettingsService;
    }

    [HttpPost("conference-settings")]
    public async Task<IActionResult> Create(int conferenceId, [FromBody] ConferenceSettingsCreateDto dto)
    {
        var settings = await _conferenceSettingsService.CreateAsync(conferenceId, dto);
        if (settings == null)
            return NotFound();

        return Ok(settings);
    }

    [HttpPut("conference-settings/{importantDateId:int}")]
    public async Task<IActionResult> Update(int conferenceId, int importantDateId, [FromBody] ImportantDatesUpdatedDateDto dto)
    {
        var importantDate = await _conferenceSettingsService.UpdateAsync(conferenceId, importantDateId, dto);
        if (importantDate == null)
            return NotFound();

        return Ok(importantDate);
    }

    [HttpDelete("conference-settings/{importantDateId:int}")]
    public async Task<IActionResult> Delete(int conferenceId, int importantDateId)
    {
        var deleted = await _conferenceSettingsService.DeleteAsync(conferenceId, importantDateId);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    [HttpPost("conference-settings/conference-entries")]
    public async Task<IActionResult> CreateConferenceEntries(int conferenceId, [FromBody] ConferenceEntryCreateRequestDto dto)
    {
        var settings = await _conferenceSettingsService.CreateConferenceEntriesAsync(conferenceId, dto);
        if (settings == null)
            return NotFound();

        return Ok(settings);
    }

    [HttpPut("conference-settings/conference-entries/{conferenceEntryId:int}")]
    public async Task<IActionResult> UpdateConferenceEntry(int conferenceId, int conferenceEntryId, [FromBody] ConferenceEntryUpdateDto dto)
    {
        var conferenceEntry = await _conferenceSettingsService.UpdateConferenceEntryAsync(conferenceId, conferenceEntryId, dto);
        if (conferenceEntry == null)
            return NotFound();

        return Ok(conferenceEntry);
    }

    [HttpDelete("conference-settings/conference-entries/{conferenceEntryId:int}")]
    public async Task<IActionResult> DeleteConferenceEntry(int conferenceId, int conferenceEntryId)
    {
        var deleted = await _conferenceSettingsService.DeleteConferenceEntryAsync(conferenceId, conferenceEntryId);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    [HttpPost("conference-settings/food-options")]
    public async Task<IActionResult> CreateFoodOptions(int conferenceId, [FromBody] FoodOptionsCreateRequestDto dto)
    {
        var settings = await _conferenceSettingsService.CreateFoodOptionsAsync(conferenceId, dto);
        if (settings == null)
            return NotFound();

        return Ok(settings);
    }

    [HttpPut("conference-settings/food-options/{foodOptionId:int}")]
    public async Task<IActionResult> UpdateFoodOption(int conferenceId, int foodOptionId, [FromBody] FoodOptionsUpdateDto dto)
    {
        var foodOption = await _conferenceSettingsService.UpdateFoodOptionAsync(conferenceId, foodOptionId, dto);
        if (foodOption == null)
            return NotFound();

        return Ok(foodOption);
    }

    [HttpDelete("conference-settings/food-options/{foodOptionId:int}")]
    public async Task<IActionResult> DeleteFoodOption(int conferenceId, int foodOptionId)
    {
        var deleted = await _conferenceSettingsService.DeleteFoodOptionAsync(conferenceId, foodOptionId);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    [HttpPost("conference-settings/booking-options")]
    public async Task<IActionResult> CreateBookingOptions(int conferenceId, [FromBody] BookingOptionsCreateRequestDto dto)
    {
        var settings = await _conferenceSettingsService.CreateBookingOptionsAsync(conferenceId, dto);
        if (settings == null)
            return NotFound();

        return Ok(settings);
    }

    [HttpPut("conference-settings/booking-options/{bookingOptionId:int}")]
    public async Task<IActionResult> UpdateBookingOption(int conferenceId, int bookingOptionId, [FromBody] BookingOptionsUpdateDto dto)
    {
        var bookingOption = await _conferenceSettingsService.UpdateBookingOptionAsync(conferenceId, bookingOptionId, dto);
        if (bookingOption == null)
            return NotFound();

        return Ok(bookingOption);
    }

    [HttpDelete("conference-settings/booking-options/{bookingOptionId:int}")]
    public async Task<IActionResult> DeleteBookingOption(int conferenceId, int bookingOptionId)
    {
        var deleted = await _conferenceSettingsService.DeleteBookingOptionAsync(conferenceId, bookingOptionId);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    [HttpPut("conference-settings/program")]
    public async Task<IActionResult> ReplaceProgram(int conferenceId, [FromBody] ProgramReplaceRequestDto dto)
    {
        var settings = await _conferenceSettingsService.ReplaceProgramAsync(conferenceId, dto);
        if (settings == null)
            return NotFound();

        return Ok(settings);
    }
}
