using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs.Invoice;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/pdf")]
public class PdfController : ControllerBase
{
    private readonly IPdfService _pdfService;

    public PdfController(IPdfService pdfService)
    {
        _pdfService = pdfService;
    }
    
    [HttpPost("create-invoice/{invoiceId:int}")]
    public async Task<IActionResult> CreateInvoice([FromRoute] int invoiceId)
    {
        var pdf = await _pdfService.GeneratePdf(invoiceId);
        return File(pdf, "application/pdf", "invoice.pdf");
    }

    [HttpPost("create-program/{conferenceId:int}")]
    public async Task<IActionResult> CreateProgram([FromRoute] int conferenceId)
    {
        var pdf = await _pdfService.GenerateProgramPdf(conferenceId);
        return File(pdf, "application/pdf", "program.pdf");
    }
}
