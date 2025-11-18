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
        if (invoice == null) throw new NullReferenceException($"Invoice with id {id} not found.");
        await CalculateTotalPrice(invoice);
        return _mapper.Map<InvoiceDto?>(invoice);
    }

    public async Task<InvoiceDto> CreateAsync(CreateInvoiceDto createInvoiceDto)
    {
        var invoice = _mapper.Map<Invoice>(createInvoiceDto);
        await CalculateTotalPrice(invoice);
        await CalculateAllInvoices(invoice);
        await _invoiceRepository.AddAsync(invoice);
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

    private async Task<int> CalculateAllInvoices(Invoice invoice)
    {
        var invoiceCount = await _invoiceRepository.GetAllSumAsync(invoice.IssueDate);
        invoice.InvoiceNumber = $"{invoiceCount+1}/{invoice.IssueDate.ToString("yyyy")}";
        return invoiceCount;
    }
}