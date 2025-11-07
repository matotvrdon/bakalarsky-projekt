using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    
    private readonly IInvoiceService _service;
    
    public InvoiceController(IInvoiceService service)
    {
        _service = service;
    }

    [HttpGet("{id}", Name = "GetInvoiceById")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var invoice = await _service.GetByIdAsync(id);

        if(invoice == null) {
            return NotFound();
        }

        return Ok(invoice);
    }
    
}