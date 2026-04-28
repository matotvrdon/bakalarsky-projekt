using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs.Committees;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/committees")]
public class CommitteeController : ControllerBase
{
    private readonly ICommitteeService _committeeService;

    public CommitteeController(ICommitteeService committeeService)
    {
        _committeeService = committeeService;
    }

    [HttpGet("active")]
    public async Task<ActionResult<ConferenceCommitteesDto>> GetByActiveConference()
    {
        ConferenceCommitteesDto? result = await _committeeService.GetByActiveConferenceAsync();

        if (result == null)
        {
            return Ok(new ConferenceCommitteesDto());
        }

        return Ok(result);
    }

    [HttpGet("conference/{conferenceId:int}")]
    public async Task<ActionResult<ConferenceCommitteesDto>> GetByConferenceId(int conferenceId)
    {
        ConferenceCommitteesDto? result = await _committeeService.GetByConferenceIdAsync(conferenceId);

        if (result == null)
        {
            return Ok(new ConferenceCommitteesDto
            {
                ConferenceId = conferenceId
            });
        }

        return Ok(result);
    }

    [HttpPost("conference/{conferenceId:int}")]
    public async Task<ActionResult<CommitteeDto>> CreateCommittee(
        int conferenceId,
        [FromBody] CommitteeCreateDto dto)
    {
        CommitteeDto? result = await _committeeService.CreateCommitteeAsync(conferenceId, dto);

        if (result == null)
        {
            return BadRequest("Conference settings were not found or input is invalid.");
        }

        return Ok(result);
    }

    [HttpPut("{committeeId:int}")]
    public async Task<ActionResult<CommitteeDto>> UpdateCommittee(
        int committeeId,
        [FromBody] CommitteeUpdateDto dto)
    {
        CommitteeDto? result = await _committeeService.UpdateCommitteeAsync(committeeId, dto);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{committeeId:int}")]
    public async Task<IActionResult> DeleteCommittee(int committeeId)
    {
        bool deleted = await _committeeService.DeleteCommitteeAsync(committeeId);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("{committeeId:int}/roles")]
    public async Task<ActionResult<CommitteeRoleDto>> CreateRole(
        int committeeId,
        [FromBody] CommitteeRoleCreateDto dto)
    {
        CommitteeRoleDto? result = await _committeeService.CreateRoleAsync(committeeId, dto);

        if (result == null)
        {
            return BadRequest("Committee was not found or input is invalid.");
        }

        return Ok(result);
    }

    [HttpPut("roles/{roleId:int}")]
    public async Task<ActionResult<CommitteeRoleDto>> UpdateRole(
        int roleId,
        [FromBody] CommitteeRoleUpdateDto dto)
    {
        CommitteeRoleDto? result = await _committeeService.UpdateRoleAsync(roleId, dto);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("roles/{roleId:int}")]
    public async Task<IActionResult> DeleteRole(int roleId)
    {
        bool deleted = await _committeeService.DeleteRoleAsync(roleId);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("roles/{roleId:int}/members")]
    public async Task<ActionResult<CommitteeMemberDto>> CreateMember(
        int roleId,
        [FromBody] CommitteeMemberCreateDto dto)
    {
        CommitteeMemberDto? result = await _committeeService.CreateMemberAsync(roleId, dto);

        if (result == null)
        {
            return BadRequest("Role was not found or input is invalid.");
        }

        return Ok(result);
    }

    [HttpPut("members/{memberId:int}")]
    public async Task<ActionResult<CommitteeMemberDto>> UpdateMember(
        int memberId,
        [FromBody] CommitteeMemberUpdateDto dto)
    {
        CommitteeMemberDto? result = await _committeeService.UpdateMemberAsync(memberId, dto);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("members/{memberId:int}")]
    public async Task<IActionResult> DeleteMember(int memberId)
    {
        bool deleted = await _committeeService.DeleteMemberAsync(memberId);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}