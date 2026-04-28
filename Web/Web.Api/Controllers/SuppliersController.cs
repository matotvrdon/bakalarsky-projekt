using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/suppliers")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet("active")]
    public async Task<ActionResult<SupplierDto>> GetActive()
    {
        var supplier = await _supplierService.GetAsync();

        if (supplier == null)
        {
            return NotFound();
        }

        return Ok(supplier);
    }
}