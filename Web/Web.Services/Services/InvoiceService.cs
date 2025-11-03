using AutoMapper;
using Web.Domain.Abstractions;
using Web.Services.Abstractions;
using Web.Services.DTOs.Invoice;

namespace Web.Services.Services;

public class InvoiceService : IInvoiceService
{
    
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IMapper _mapper;

    public InvoiceService(IInvoiceRepository invoiceRepository, IMapper mapper)
    {
        _invoiceRepository = invoiceRepository;
        _mapper = mapper;
    }
    
    public async Task<InvoiceDto?> GetByIdAsync(int id)
    {
        return _mapper.Map<InvoiceDto?>(await _invoiceRepository.GetByIdAsync(id));
    }
}