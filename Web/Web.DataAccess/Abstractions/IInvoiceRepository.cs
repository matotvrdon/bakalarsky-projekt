using Web.Domain.Models;

namespace Web.DataAccess.Abstractions;

public interface IInvoiceRepository
{
    Task<Invoice?> GetByIdAsync(int id);

    Task<Invoice?> GetBySharedCodeAsync(string sharedCode);

    Task<List<Invoice>> GetByParticipantIdAsync(int participantId);

    Task<List<Invoice>> GetAllAsync();

    Task<Participant?> GetParticipantWithInvoiceDataAsync(int participantId);

    Task<int> GetInvoiceCountForYearAsync(int year);

    Task<bool> SharedCodeExistsAsync(string sharedCode);

    Task AddAsync(Invoice invoice);

    Task UpdateAsync(Invoice invoice);
}