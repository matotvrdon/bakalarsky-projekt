using Web.Services.DTOs;

namespace Web.Services.Abstractions;

public interface IInvoiceService
{
    Task<InvoiceDto?> GetByIdAsync(int id);

    Task<List<InvoiceDto>> GetAllAsync();

    Task<List<InvoiceDto>> GetByParticipantIdAsync(int participantId);

    Task<InvoiceDto> CreateAsync(InvoiceCreateDto dto);

    Task<InvoiceDto?> JoinSharedAsync(JoinSharedInvoiceDto dto);

    Task<InvoiceDto?> UpdateStatusAsync(int invoiceId, InvoiceStatusUpdateDto dto);
}