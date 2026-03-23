using AutoMapper;
using System.Net;
using Web.DataAccess.Abstractions;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs;
using Web.Services.Exceptions;

namespace Web.Services.Services;

public class SubmissionService : ISubmissionService
{
    private readonly ISubmissionRepository _submissionRepository;
    private readonly IParticipantRepository _participantRepository;
    private readonly IConferenceRepository _conferenceRepository;
    private readonly IMapper _mapper;

    public SubmissionService(
        ISubmissionRepository submissionRepository,
        IParticipantRepository participantRepository,
        IConferenceRepository conferenceRepository,
        IMapper mapper)
    {
        _submissionRepository = submissionRepository;
        _participantRepository = participantRepository;
        _conferenceRepository = conferenceRepository;
        _mapper = mapper;
    }

    public async Task<SubmissionDto> CreateAsync(SubmissionCreateDto dto)
    {
        var participant = await GetParticipantOrThrowAsync(dto.ParticipantId);
        await EnsureConferenceExistsAsync(dto.ConferenceId);
        EnsureParticipantConferenceMatch(participant, dto.ConferenceId);

        if (await _submissionRepository.ExistsByParticipantIdAsync(dto.ParticipantId))
        {
            throw new SubmissionFlowException(HttpStatusCode.Conflict, new SubmissionErrorResponseDto
            {
                Code = "SUBMISSION_ALREADY_EXISTS",
                Message = "Submission for this participant already exists. Use update flow.",
                Field = "participantId"
            });
        }

        var submission = _mapper.Map<Submission>(dto);
        submission.Participant = participant;
        submission.CreatedAt = DateTime.UtcNow;
        submission.UpdatedAt = null;

        await _submissionRepository.AddAsync(submission);
        return _mapper.Map<SubmissionDto>(submission);
    }

    public async Task<SubmissionDto?> UpdateAsync(int id, SubmissionUpdateDto dto)
    {
        var submission = await _submissionRepository.GetByIdAsync(id);
        if (submission == null)
            return null;

        var participant = await GetParticipantOrThrowAsync(dto.ParticipantId);
        await EnsureConferenceExistsAsync(dto.ConferenceId);
        EnsureParticipantConferenceMatch(participant, dto.ConferenceId);

        var existingForParticipant = await _submissionRepository.GetByParticipantIdAsync(dto.ParticipantId);
        if (existingForParticipant != null && existingForParticipant.Id != id)
        {
            throw new SubmissionFlowException(HttpStatusCode.Conflict, new SubmissionErrorResponseDto
            {
                Code = "SUBMISSION_ALREADY_EXISTS",
                Message = "Submission for this participant already exists.",
                Field = "participantId"
            });
        }

        _mapper.Map(dto, submission);
        submission.Participant = participant;
        submission.UpdatedAt = DateTime.UtcNow;

        await _submissionRepository.UpdateAsync(submission);
        return _mapper.Map<SubmissionDto>(submission);
    }

    public async Task<SubmissionDto?> GetByParticipantIdAsync(int participantId)
    {
        var submission = await _submissionRepository.GetByParticipantIdAsync(participantId);
        return submission == null ? null : _mapper.Map<SubmissionDto>(submission);
    }

    private async Task<Participant> GetParticipantOrThrowAsync(int participantId)
    {
        var participant = await _participantRepository.GetByIdAsync(participantId);
        if (participant != null)
            return participant;

        throw new SubmissionFlowException(HttpStatusCode.NotFound, new SubmissionErrorResponseDto
        {
            Code = "PARTICIPANT_NOT_FOUND",
            Message = $"Participant with id {participantId} was not found.",
            Field = "participantId"
        });
    }

    private async Task EnsureConferenceExistsAsync(int conferenceId)
    {
        var conference = await _conferenceRepository.GetByIdAsync(conferenceId);
        if (conference != null)
            return;

        throw new SubmissionFlowException(HttpStatusCode.NotFound, new SubmissionErrorResponseDto
        {
            Code = "CONFERENCE_NOT_FOUND",
            Message = $"Conference with id {conferenceId} was not found.",
            Field = "conferenceId"
        });
    }

    private static void EnsureParticipantConferenceMatch(Participant participant, int conferenceId)
    {
        if (participant.ConferenceId == conferenceId)
            return;

        throw new SubmissionFlowException(HttpStatusCode.Conflict, new SubmissionErrorResponseDto
        {
            Code = "PARTICIPANT_CONFERENCE_MISMATCH",
            Message = "Participant does not belong to the provided conference.",
            Field = "conferenceId"
        });
    }
}
