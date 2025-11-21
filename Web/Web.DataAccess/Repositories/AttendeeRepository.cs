using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Data;
using Web.Domain.Abstractions;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class AttendeeRepository : IAttendeeRepository
{
    private readonly ApplicationDbContext _context;

    public AttendeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Attendee>> GetAllByCustomerIdAsync(int customerId)
    {
        return await _context.Attendee
            .Include(a => a.InvoiceItem)
            .Include(a => a.Customer)
            .Where(a => a.CustomerId == customerId)
            .ToListAsync(); 
    }

    public async Task<List<Attendee?>> GetAllByAttendeeIdAsync(List<int> listAttendeeId)
    {
        List<Attendee?> attendees = new();
        foreach(var attendeeId in listAttendeeId)
        {
            attendees.Add(await GetAttendeeByIdAsync(attendeeId));
        }
        return attendees;
    }

    public async Task AddAsync(Attendee attendee)
    {
        await _context.Attendee.AddAsync(attendee);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Attendee attendee)
    {
        _context.Attendee.Update(attendee);
        await _context.SaveChangesAsync();
    }

    public async Task<Attendee?> GetAttendeeByIdAsync(int id)
    {
        return await _context.Attendee
            .Include(a => a.InvoiceItem)
            .Include(a => a.Customer)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}