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

    public async Task<Invoice?> GetByIdAsync(int id)
    {
        var invoice = await _context.Invoice
            .Include(i => i.Customer)
            .Include(i => i.Supplier)
            .Include(i => i.InvoiceItem)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (invoice != null)
        {
            invoice.InvoiceItem = invoice.InvoiceItem.OrderBy(ii => ii.Id).ToList();
        }

        return invoice;
    }
}