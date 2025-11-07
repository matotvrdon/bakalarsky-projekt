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
    private readonly IInvoiceItemService _invoiceItemService;

    public PdfController(IPdfService pdfService, ISupplierService supplierService, IInvoiceItemService invoiceItemService)
    {
        _pdfService = pdfService;
        _supplierService = supplierService;
        _invoiceItemService = invoiceItemService;
    }
    
    [HttpPost("create/{invoiceId}")]
    public async Task<IActionResult> CreateInvoice([FromRoute] int invoiceId)
    {
        var pdf = await _pdfService.GeneratePdf(invoiceId);
        return File(pdf, "application/pdf", "invoice.pdf");
    }
}