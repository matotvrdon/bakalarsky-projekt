using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Abstractions;
using Web.DataAccess.Data;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly AppDbContext _context;

    public InvoiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Invoice?> GetByIdAsync(int id)
    {
        return await _context.Invoices
            .Include(invoice => invoice.FileManager)
            .Include(invoice => invoice.Items)
                .ThenInclude(item => item.Participant)
            .Include(invoice => invoice.Participants)
                .ThenInclude(invoiceParticipant => invoiceParticipant.Participant)
            .FirstOrDefaultAsync(invoice => invoice.Id == id);
    }

    public async Task<Invoice?> GetBySharedCodeAsync(string sharedCode)
    {
        return await _context.Invoices
            .Include(invoice => invoice.FileManager)
            .Include(invoice => invoice.Items)
                .ThenInclude(item => item.Participant)
            .Include(invoice => invoice.Participants)
                .ThenInclude(invoiceParticipant => invoiceParticipant.Participant)
            .FirstOrDefaultAsync(invoice => invoice.SharedCode == sharedCode);
    }

    public async Task<List<Invoice>> GetByParticipantIdAsync(int participantId)
    {
        return await _context.Invoices
            .Include(invoice => invoice.FileManager)
            .Include(invoice => invoice.Items)
                .ThenInclude(item => item.Participant)
            .Include(invoice => invoice.Participants)
                .ThenInclude(invoiceParticipant => invoiceParticipant.Participant)
            .Where(invoice => invoice.Participants.Any(item => item.ParticipantId == participantId))
            .OrderByDescending(invoice => invoice.CreatedAtUtc)
            .ToListAsync();
    }

    public async Task<List<Invoice>> GetAllAsync()
    {
        return await _context.Invoices
            .Include(invoice => invoice.FileManager)
            .Include(invoice => invoice.Items)
                .ThenInclude(item => item.Participant)
            .Include(invoice => invoice.Participants)
                .ThenInclude(invoiceParticipant => invoiceParticipant.Participant)
            .OrderByDescending(invoice => invoice.CreatedAtUtc)
            .ToListAsync();
    }

    public async Task<Participant?> GetParticipantWithInvoiceDataAsync(int participantId)
    {
        return await _context.Participants
            .Include(participant => participant.ConferenceEntry)
            .Include(participant => participant.Conference)
                .ThenInclude(conference => conference!.Settings)
                    .ThenInclude(settings => settings!.ConferenceEntries)
            .Include(participant => participant.Conference)
                .ThenInclude(conference => conference!.Settings)
                    .ThenInclude(settings => settings!.BookingOptions)
            .Include(participant => participant.Conference)
                .ThenInclude(conference => conference!.Settings)
                    .ThenInclude(settings => settings!.FoodOptions)
            .FirstOrDefaultAsync(participant => participant.Id == participantId);
    }

    public async Task<int> GetInvoiceCountForYearAsync(int year)
    {
        return await _context.Invoices
            .CountAsync(invoice => invoice.CreatedAtUtc.Year == year);
    }

    public async Task<bool> SharedCodeExistsAsync(string sharedCode)
    {
        return await _context.Invoices
            .AnyAsync(invoice => invoice.SharedCode == sharedCode);
    }

    public async Task AddAsync(Invoice invoice)
    {
        await _context.Invoices.AddAsync(invoice);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Invoice invoice)
    {
        _context.Invoices.Update(invoice);
        await _context.SaveChangesAsync();
    }
}