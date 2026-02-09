using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Data;
using Web.Domain.Abstractions;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class InvoiceItemRepository : IInvoiceItemRepository
{
    private readonly ApplicationDbContext _context;

    public InvoiceItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<InvoiceItem?> GetByIdAsync(int invoiceItemId)
    {
        return await _context.InvoiceItem
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == invoiceItemId);
    }

    public async Task AddAsync(InvoiceItem invoiceItem)
    {
        await _context.InvoiceItem.AddAsync(invoiceItem);
        await _context.SaveChangesAsync();
    }
}
