using AutoMapper;
using Web.Domain.Abstractions;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs.InvoiceItem;

namespace Web.Services.Services;

public class InvoiceItemService : IInvoiceItemService
{
    private readonly IInvoiceItemRepository _repository;
    private readonly IInvoiceService _invoiceService;
    private readonly IMapper _mapper;

    public InvoiceItemService(IInvoiceItemRepository repository, IMapper mapper, IInvoiceService invoiceService)
    {
        _repository = repository;
        _mapper = mapper;
        _invoiceService = invoiceService;
    }

    public async Task<List<InvoiceItemDto>> GetAllByInvoiceIdAsync(int invoiceDtoId)
    {
        return _mapper.Map<List<InvoiceItemDto>>(await _repository.GetAllByInvoiceIdAsync(invoiceDtoId));
    }

    public async Task<InvoiceItemDto?> GetByIdAsync(int invoiceItemDtoId)
    {
        return _mapper.Map<InvoiceItemDto?>(await _repository.GetByIdAsync(invoiceItemDtoId));
    }

    public async Task<InvoiceItemDto> AddAsync(CreateInvoiceItemDto createInvoiceItemDto)
    {
        var invoiceItem = _mapper.Map<InvoiceItem>(createInvoiceItemDto);
        invoiceItem.Price = invoiceItem.Quantity * createInvoiceItemDto.UnitPrice;
        await _repository.AddAsync(invoiceItem);
        return _mapper.Map<InvoiceItemDto>(invoiceItem);
    }
}