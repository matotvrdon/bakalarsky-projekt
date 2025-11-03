using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs.Invoice;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PdfController : ControllerBase
{

    private readonly IPdfService _pdf;

    public PdfController(IPdfService pdf)
    {
        _pdf = pdf;
    }
    
    [HttpPost("invoice")]
    public async Task<IActionResult> Generate([FromBody] InvoiceDto dto, CancellationToken ct)
    {
        var bytes = await _pdf.GenerateInvoicePdfAsync(dto);
        return File(bytes, "application/pdf", "invoice.pdf");
    }
}