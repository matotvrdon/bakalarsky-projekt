using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs.InvoiceItem;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/invoice-item")]
public class InvoiceItemController : ControllerBase
{
    private readonly IInvoiceItemService _invoiceItemService;
    private readonly IInvoiceService _invoiceService;

    public InvoiceItemController(IInvoiceItemService invoiceItemService, IInvoiceService invoiceService)
    {
        _invoiceItemService = invoiceItemService;
        _invoiceService = invoiceService;
    }

    [HttpGet("{invoiceItemId:int}", Name = "GetInvoiceItemById")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int invoiceItemId)
    {
        var invoiceItem = await _invoiceItemService.GetByIdAsync(invoiceItemId);

        if(invoiceItem == null) {
            return NotFound($"Invoice item with id {invoiceItemId} not found.");
        }

        return Ok(invoiceItem);
    }


    [HttpPost("create-invoice-item")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateInvoiceItemDto createInvoiceItemDto)
    {
        var result = await _invoiceItemService.AddAsync(createInvoiceItemDto);
        return CreatedAtAction("GetById", new { invoiceItemId = result.Id }, result);
    }
}