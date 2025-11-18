using Microsoft.EntityFrameworkCore;
using Web.Domain.Models;
using Laraue.EfCoreTriggers.Common.Extensions;

namespace Web.DataAccess.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        // triggers are configured in OnModelCreating using Laraue
    }
    
    public DbSet<Conference> Conference { get; set; }
    public DbSet<Invoice> Invoice { get; set; }
    public DbSet<Supplier> Supplier { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<InvoiceItem> InvoiceItem { get; set; }
    public DbSet<Attendee> Attendee { get; set; }
    public DbSet<Day> Day { get; set; }
    public DbSet<Session> Session { get; set; }
    public DbSet<Theme> Theme { get; set; }
    public DbSet<Talk> Talk { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Supplier>(e =>
        {
            e.HasData(
                new Supplier {
                    Id = 1,
                    Name = "Pobočka SSAKI pri KPI FEI TU v Košiciach",
                    Street = "Letná 9",
                    City = "Košice",
                    PostalCode = "040 01",
                    Country = "Slovanská Republika",
                    Ico = "35541784",
                    Dic = "2021681970",
                    IcDph = "SK2021681970",
                    Bank = "Všeobecná úverová banka, a.s.",
                    Address = "Letná 40",
                    AddressPostalCode = "040 01",
                    AddressCity = "Košice",
                    BankAccount = "1584034953/0200",
                    Swift = "SUBASKBX",
                    Iban = "SK55 0200 0000 0015 8403 4953",
                    Phone = "+421/(0)55/602 4148"
                }
            );
        });

        modelBuilder.Entity<Customer>(e =>
        {
            e.HasData(
                new Customer {
                    Id = 1,
                    Name = "Martn Tvrdoň",
                    Street = "Humenská 3",
                    City = "Košice",
                    PostalCode = "040 11",
                    Country = "Slovanská Republika",
                }
            );
        });

    }
}