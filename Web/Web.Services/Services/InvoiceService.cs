using AutoMapper;
using Web.Domain.Abstractions;
using Web.Domain.Models;
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
        var invoice = await _invoiceRepository.GetByIdAsync(id);
        if (invoice == null)
        {
            return null;
        }
        await CalculateTotalPrice(invoice);
        return _mapper.Map<InvoiceDto>(invoice);
    }

    public async Task<InvoiceDto> CreateAsync(CreateInvoiceDto createInvoiceDto)
    {
        var invoice = _mapper.Map<Invoice>(createInvoiceDto);
        await CalculateTotalPrice(invoice);
        await _invoiceRepository.AddWithNumberAsync(invoice);
        return _mapper.Map<InvoiceDto>(invoice);
    }
    
    
    private async Task<decimal> CalculateTotalPrice(Invoice invoice)
    {
        var totalPrice = invoice.Customer?.Attendee?.SelectMany(a => a.InvoiceItem).Sum(a => a?.Price) ?? 0m;
        if (invoice.TotalPrice != totalPrice)
        {
            invoice.TotalPrice = totalPrice;
            await _invoiceRepository.UpdateAsync(invoice);
        }
        return totalPrice;
    }

}
