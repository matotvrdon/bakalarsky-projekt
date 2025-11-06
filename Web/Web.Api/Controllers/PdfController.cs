using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs.Invoice;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/pdf")]
public class PdfController : ControllerBase
{
    private readonly IPdfService _pdfService;
    private readonly ISupplierService _supplierService;

    public PdfController(IPdfService pdfService, ISupplierService supplierService)
    {
        _pdfService = pdfService;
        _supplierService = supplierService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceDto dto)
    {
        var pdf = await _pdfService.GeneratePdf(dto);
        return File(pdf, "application/pdf", "invoice.pdf");
    }
}