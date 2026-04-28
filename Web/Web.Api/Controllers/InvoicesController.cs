using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/invoices")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;
    private readonly IInvoicePdfGenerator _invoicePdfGenerator;
    private readonly IFileManagerService _fileManagerService;

    public InvoicesController(
        IInvoiceService invoiceService,
        IInvoicePdfGenerator invoicePdfGenerator,
        IFileManagerService fileManagerService)
    {
        _invoiceService = invoiceService;
        _invoicePdfGenerator = invoicePdfGenerator;
        _fileManagerService = fileManagerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<InvoiceDto>>> GetAll()
    {
        var invoices = await _invoiceService.GetAllAsync();

        return Ok(invoices);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<InvoiceDto>> GetById(int id)
    {
        var invoice = await _invoiceService.GetByIdAsync(id);

        if (invoice == null)
        {
            return NotFound();
        }

        return Ok(invoice);
    }

    [HttpGet("participant/{participantId:int}")]
    public async Task<ActionResult<List<InvoiceDto>>> GetByParticipantId(
        int participantId)
    {
        var invoices = await _invoiceService.GetByParticipantIdAsync(participantId);

        return Ok(invoices);
    }

    [HttpPost]
    public async Task<ActionResult<InvoiceDto>> Create(InvoiceCreateDto dto)
    {
        var invoice = await _invoiceService.CreateAsync(dto);

        return Ok(invoice);
    }

    [HttpPost("join-shared")]
    public async Task<ActionResult<InvoiceDto>> JoinShared(JoinSharedInvoiceDto dto)
    {
        var invoice = await _invoiceService.JoinSharedAsync(dto);

        if (invoice == null)
        {
            return NotFound("Shared invoice was not found.");
        }

        return Ok(invoice);
    }

    [HttpPatch("{id:int}/status")]
    public async Task<ActionResult<InvoiceDto>> UpdateStatus(
        int id,
        InvoiceStatusUpdateDto dto)
    {
        var invoice = await _invoiceService.UpdateStatusAsync(id, dto);

        if (invoice == null)
        {
            return NotFound();
        }

        return Ok(invoice);
    }

    [HttpGet("{id:int}/pdf")]
    public async Task<IActionResult> DownloadPdf(int id)
    {
        var invoice = await _invoiceService.GetByIdAsync(id);

        if (invoice == null)
        {
            return NotFound();
        }

        if (invoice.FileManagerId.HasValue)
        {
            var storedFile = await _fileManagerService.DownloadAsync(
                invoice.FileManagerId.Value
            );

            return PhysicalFile(
                storedFile.FilePath,
                storedFile.ContentType,
                storedFile.FileName,
                enableRangeProcessing: true
            );
        }

        var pdf = await _invoicePdfGenerator.GeneratePdfAsync(id);
        var fileName = $"{invoice.InvoiceNumber}.pdf";

        return File(pdf, "application/pdf", fileName);
    }
}