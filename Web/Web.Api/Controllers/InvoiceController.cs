using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs.Invoice;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/invoice")]
public class InvoiceController : ControllerBase
{
    
    private readonly IInvoiceService _service;
    
    public InvoiceController(IInvoiceService service)
    {
        _service = service;
    }

    [HttpGet("{invoiceId}", Name = "GetInvoiceById")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int invoiceId)
    {
        var invoice = await _service.GetByIdAsync(invoiceId);

        if(invoice == null) {
            return NotFound();
        }

        return Ok(invoice);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateInvoiceDto createInvoiceDto)
    {
        var invoice = await _service.CreateAsync(createInvoiceDto);
        return CreatedAtAction("GetById", new { invoiceId = invoice.Id }, invoice);
    }
}