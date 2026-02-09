using System.Linq;
using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Data;
using Web.Domain.Abstractions;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    
    private readonly ApplicationDbContext _context;

    public InvoiceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetAllSumAsync(DateTime date)
    {
        return await _context.Invoice
            .AsNoTracking()
            .Where(i => i.IssueDate.Year == date.Year)
            .CountAsync();
    }

    public async Task<Invoice?> GetByIdAsync(int id)
    {
        var invoice = await _context.Invoice
            .AsNoTracking()
            .Include(i => i.Customer).ThenInclude(c => c.Attendee).ThenInclude(a => a.InvoiceItem)
            .Include(i => i.Supplier)
            .FirstOrDefaultAsync(x => x.Id == id);

        return invoice;
    }

    public async Task AddAsync(Invoice invoice)
    {
        await _context.Invoice.AddAsync(invoice);
        await _context.SaveChangesAsync();
    }

    public async Task AddWithNumberAsync(Invoice invoice)
    {
        var year = invoice.IssueDate.Year;

        await using var transaction = await _context.Database.BeginTransactionAsync();
        await _context.Database.ExecuteSqlRawAsync("SELECT pg_advisory_xact_lock({0})", year);

        var invoiceCount = await _context.Invoice
            .AsNoTracking()
            .Where(i => i.IssueDate.Year == year)
            .CountAsync();

        invoice.InvoiceNumber = $"{invoiceCount + 1}/{year}";

        await _context.Invoice.AddAsync(invoice);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    public async Task UpdateAsync(Invoice invoice)
    {
        _context.Invoice.Update(invoice);
        await _context.SaveChangesAsync();
    }

}
