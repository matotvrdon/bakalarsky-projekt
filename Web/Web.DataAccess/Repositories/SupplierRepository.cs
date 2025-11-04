using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Data;
using Web.Domain.Abstractions;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class SupplierRepository : ISupplierInterface
{
    
    private readonly ApplicationDbContext _context;

    public SupplierRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Supplier?> GetByIdAsync(int id)
    {
        return await _context.Supplier
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}