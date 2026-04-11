using Web.Domain.Models;

namespace Web.Services.Abstractions;

public interface IProgramPdfGenerator
{
    byte[] GenerateProgramPdf(ConferenceSettings conferenceSettings, List<ProgramDay> programDays);
}
