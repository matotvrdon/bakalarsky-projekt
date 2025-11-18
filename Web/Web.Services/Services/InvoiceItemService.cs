using AutoMapper;
using Web.Domain.Abstractions;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs.InvoiceItem;

namespace Web.Services.Services;

public class InvoiceItemService : IInvoiceItemService
{
    private readonly IInvoiceItemRepository _repository;
    private readonly IMapper _mapper;

    public InvoiceItemService(IInvoiceItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<InvoiceItemDto?> GetByIdAsync(int invoiceItemId)
    {
        return _mapper.Map<InvoiceItemDto?>(await _repository.GetByIdAsync(invoiceItemId));
    }

    public async Task<InvoiceItemDto> AddAsync(CreateInvoiceItemDto createInvoiceItemDto)
    {
        var invoiceItem = _mapper.Map<InvoiceItem>(createInvoiceItemDto);
        invoiceItem.Price = invoiceItem.Quantity * invoiceItem.UnitPrice;
        await _repository.AddAsync(invoiceItem);
        return _mapper.Map<InvoiceItemDto>(invoiceItem);
    }
}