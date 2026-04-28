using Web.Services.DTOs;

namespace Web.Services.Abstractions;

public interface IProgramPdfGenerator
{
    byte[] GenerateProgramPdf(ConferenceDto conference);
}