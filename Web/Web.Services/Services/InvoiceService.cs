using AutoMapper;
using Web.DataAccess.Abstractions;
using Web.Domain.Enums;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Services.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IMapper _mapper;
    private readonly IInvoicePdfStorageService _invoicePdfStorageService;

    public InvoiceService(
        IInvoiceRepository invoiceRepository,
        IMapper mapper,
        IInvoicePdfStorageService invoicePdfStorageService)
    {
        _invoiceRepository = invoiceRepository;
        _mapper = mapper;
        _invoicePdfStorageService = invoicePdfStorageService;
    }
    
    public async Task<List<InvoiceDto>> GetAllAsync()
    {
        var invoices = await _invoiceRepository.GetAllAsync();

        return _mapper.Map<List<InvoiceDto>>(invoices);
    }

    public async Task<InvoiceDto?> GetByIdAsync(int id)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(id);

        if (invoice == null)
        {
            return null;
        }

        return _mapper.Map<InvoiceDto>(invoice);
    }

    public async Task<List<InvoiceDto>> GetByParticipantIdAsync(int participantId)
    {
        var invoices = await _invoiceRepository.GetByParticipantIdAsync(participantId);

        return _mapper.Map<List<InvoiceDto>>(invoices);
    }

    public async Task<InvoiceDto> CreateAsync(InvoiceCreateDto dto)
    {
        var participant = await _invoiceRepository.GetParticipantWithInvoiceDataAsync(dto.ParticipantId);

        if (participant == null)
        {
            throw new Exception("Participant was not found.");
        }
        
        var existingInvoices = await _invoiceRepository.GetByParticipantIdAsync(participant.Id);

        var activeInvoice = existingInvoices.FirstOrDefault(invoice =>
            invoice.Status != InvoiceStatus.Cancelled
        );

        if (activeInvoice != null)
        {
            throw new Exception("Účastník už má aktívnu faktúru. Novú faktúru je možné vytvoriť až po zrušení existujúcej faktúry.");
        }

        if (participant.Conference == null)
        {
            throw new Exception("Participant conference was not found.");
        }

        if (participant.Conference.Settings == null)
        {
            throw new Exception("Conference settings were not found.");
        }

        ValidateCustomerData(dto, participant);

        var invoice = new Invoice
        {
            ConferenceId = participant.ConferenceId,
            InvoiceNumber = await GenerateInvoiceNumberAsync(),
            Type = dto.Type,
            Status = InvoiceStatus.Pending,
            SharedCode = dto.Type == InvoiceType.Shared
                ? await GenerateUniqueSharedCodeAsync()
                : null,

            CustomerType = dto.CustomerType,
            CustomerName = ResolveCustomerName(dto, participant),
            CompanyName = dto.CustomerType == InvoiceCustomerType.Company
                ? NormalizeText(dto.CompanyName)
                : null,
            BillingAddress = NormalizeRequiredText(
                dto.BillingAddress,
                "Fakturačná adresa je povinná."
            ),
            Ico = dto.CustomerType == InvoiceCustomerType.Company
                ? NormalizeText(dto.Ico)
                : null,
            Dic = dto.CustomerType == InvoiceCustomerType.Company
                ? NormalizeText(dto.Dic)
                : null,
            VatId = dto.CustomerType == InvoiceCustomerType.Company
                ? NormalizeText(dto.VatId)
                : null,

            CreatedAtUtc = DateTime.UtcNow,
            DueDateUtc =  DateTime.UtcNow.AddMonths(1),
            Items = [],
            Participants = []
        };

        invoice.Participants.Add(new InvoiceParticipant
        {
            ParticipantId = participant.Id
        });

        AddParticipantItems(
            invoice,
            participant,
            dto.BookingOptionId,
            dto.FoodOptionIds
        );

        invoice.TotalAmount = invoice.Items.Sum(item => item.TotalPrice);

        await _invoiceRepository.AddAsync(invoice);
        
        var createdInvoice = await _invoiceRepository.GetByIdAsync(invoice.Id);

        if (createdInvoice == null)
        {
            throw new Exception("Created invoice could not be loaded.");
        }

        await _invoicePdfStorageService.StoreOrReplaceAsync(createdInvoice);

        var loadedInvoice = await _invoiceRepository.GetByIdAsync(invoice.Id);

        if (loadedInvoice == null)
        {
           throw new Exception("Created invoice could not be loaded.");
        }

        return _mapper.Map<InvoiceDto>(loadedInvoice);
    }

    public async Task<InvoiceDto?> JoinSharedAsync(JoinSharedInvoiceDto dto)
    {
        var sharedCode = NormalizeRequiredText(
            dto.SharedCode,
            "Kód zdieľanej faktúry je povinný."
        ).ToUpperInvariant();

        var invoice = await _invoiceRepository.GetBySharedCodeAsync(sharedCode);

        if (invoice == null)
        {
            return null;
        }

        if (invoice.Type != InvoiceType.Shared)
        {
            throw new Exception("Invoice is not shared.");
        }

        if (invoice.Status != InvoiceStatus.Pending)
        {
            throw new Exception("Only pending shared invoice can be joined.");
        }

        if (invoice.Participants.Any(item => item.ParticipantId == dto.ParticipantId))
        {
            throw new Exception("Participant is already assigned to this invoice.");
        }

        var participant = await _invoiceRepository.GetParticipantWithInvoiceDataAsync(dto.ParticipantId);

        var existingInvoices = await _invoiceRepository.GetByParticipantIdAsync(participant.Id);

        var activeInvoice = existingInvoices.FirstOrDefault(existingInvoice =>
            existingInvoice.Status != InvoiceStatus.Cancelled
        );

        if (activeInvoice != null)
        {
            throw new Exception("Účastník už má aktívnu faktúru. K zdieľanej faktúre sa môže pripojiť až po zrušení existujúcej faktúry.");
        }
        
        if (participant == null)
        {
            throw new Exception("Participant was not found.");
        }

        if (participant.Conference == null)
        {
            throw new Exception("Participant conference was not found.");
        }

        if (participant.Conference.Settings == null)
        {
            throw new Exception("Conference settings were not found.");
        }

        if (participant.ConferenceId != invoice.ConferenceId)
        {
            throw new Exception("Participant belongs to a different conference.");
        }

        invoice.Participants.Add(new InvoiceParticipant
        {
            ParticipantId = participant.Id
        });

        AddParticipantItems(
            invoice,
            participant,
            dto.BookingOptionId,
            dto.FoodOptionIds
        );

        invoice.TotalAmount = invoice.Items.Sum(item => item.TotalPrice);

        await _invoiceRepository.UpdateAsync(invoice);

        var updatedInvoice = await _invoiceRepository.GetByIdAsync(invoice.Id);

        if (updatedInvoice == null)
        {
            throw new Exception("Updated invoice could not be loaded.");
        }

        await _invoicePdfStorageService.StoreOrReplaceAsync(updatedInvoice);
        
        var loadedInvoice = await _invoiceRepository.GetByIdAsync(invoice.Id);

        if (loadedInvoice == null)
        {
            throw new Exception("Updated invoice could not be loaded.");
        }

        return _mapper.Map<InvoiceDto>(loadedInvoice);
    }

    public async Task<InvoiceDto?> UpdateStatusAsync(
        int invoiceId,
        InvoiceStatusUpdateDto dto)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);

        if (invoice == null)
        {
            return null;
        }

        invoice.Status = dto.Status;

        if (dto.Status == InvoiceStatus.Paid)
        {
            invoice.PaidAtUtc = DateTime.UtcNow;
        }
        else
        {
            invoice.PaidAtUtc = null;
        }

        await _invoiceRepository.UpdateAsync(invoice);

        var loadedInvoice = await _invoiceRepository.GetByIdAsync(invoice.Id);

        if (loadedInvoice == null)
        {
            throw new Exception("Updated invoice could not be loaded.");
        }

        return _mapper.Map<InvoiceDto>(loadedInvoice);
    }

    private void AddParticipantItems(
        Invoice invoice,
        Participant participant,
        int? bookingOptionId,
        List<int> foodOptionIds)
    {
        var settings = participant.Conference?.Settings;

        if (settings == null)
        {
            throw new Exception("Conference settings were not found.");
        }

        AddConferenceEntryItem(invoice, participant, settings);
        AddBookingOptionItem(invoice, participant, settings, bookingOptionId);
        AddFoodOptionItems(invoice, participant, settings, foodOptionIds);
    }

    private static void AddConferenceEntryItem(
        Invoice invoice,
        Participant participant,
        ConferenceSettings settings)
    {
        if (!participant.ConferenceEntryId.HasValue)
        {
            return;
        }

        var conferenceEntry = settings.ConferenceEntries?
            .FirstOrDefault(entry => entry.Id == participant.ConferenceEntryId.Value);

        if (conferenceEntry == null)
        {
            throw new Exception("Conference entry was not found in conference settings.");
        }

        var price = ConvertToDecimal(conferenceEntry.Price);

        invoice.Items.Add(new InvoiceItem
        {
            ParticipantId = participant.Id,
            Type = InvoiceItemType.ConferenceEntry,
            SourceId = conferenceEntry.Id,
            Name = conferenceEntry.Name,
            UnitPrice = price,
            Quantity = 1,
            TotalPrice = price
        });
    }

    private static void AddBookingOptionItem(
        Invoice invoice,
        Participant participant,
        ConferenceSettings settings,
        int? bookingOptionId)
    {
        if (!bookingOptionId.HasValue)
        {
            return;
        }

        var bookingOption = settings.BookingOptions?
            .FirstOrDefault(option => option.Id == bookingOptionId.Value);

        if (bookingOption == null)
        {
            throw new Exception("Booking option was not found in conference settings.");
        }

        var price = ConvertToDecimal(bookingOption.Price);

        invoice.Items.Add(new InvoiceItem
        {
            ParticipantId = participant.Id,
            Type = InvoiceItemType.BookingOption,
            SourceId = bookingOption.Id,
            Name = bookingOption.Name,
            UnitPrice = price,
            Quantity = 1,
            TotalPrice = price
        });
    }

    private static void AddFoodOptionItems(
        Invoice invoice,
        Participant participant,
        ConferenceSettings settings,
        List<int> foodOptionIds)
    {
        foreach (var foodOptionId in foodOptionIds.Distinct())
        {
            var foodOption = settings.FoodOptions?
                .FirstOrDefault(option => option.Id == foodOptionId);

            if (foodOption == null)
            {
                throw new Exception(
                    $"Food option with id {foodOptionId} was not found in conference settings."
                );
            }

            var price = ConvertToDecimal(foodOption.Price);

            invoice.Items.Add(new InvoiceItem
            {
                ParticipantId = participant.Id,
                Type = InvoiceItemType.FoodOption,
                SourceId = foodOption.Id,
                Name = foodOption.Name,
                UnitPrice = price,
                Quantity = 1,
                TotalPrice = price
            });
        }
    }

    private static void ValidateCustomerData(
        InvoiceCreateDto dto,
        Participant participant)
    {
        if (string.IsNullOrWhiteSpace(dto.BillingAddress))
        {
            throw new Exception("Fakturačná adresa je povinná.");
        }

        if (dto.CustomerType == InvoiceCustomerType.Person)
        {
            var customerName = ResolveCustomerName(dto, participant);

            if (string.IsNullOrWhiteSpace(customerName))
            {
                throw new Exception("Meno odberateľa je povinné.");
            }

            return;
        }

        if (dto.CustomerType == InvoiceCustomerType.Company)
        {
            if (string.IsNullOrWhiteSpace(dto.CompanyName))
            {
                throw new Exception("Názov firmy je povinný.");
            }

            if (string.IsNullOrWhiteSpace(dto.Ico))
            {
                throw new Exception("IČO je povinné pri fakturácii na firmu.");
            }

            return;
        }

        throw new Exception("Neplatný typ odberateľa.");
    }

    private static string ResolveCustomerName(
        InvoiceCreateDto dto,
        Participant participant)
    {
        if (!string.IsNullOrWhiteSpace(dto.CustomerName))
        {
            return dto.CustomerName.Trim();
        }

        return $"{participant.FirstName} {participant.LastName}".Trim();
    }

    private async Task<string> GenerateInvoiceNumberAsync()
    {
        var year = DateTime.UtcNow.Year;
        var count = await _invoiceRepository.GetInvoiceCountForYearAsync(year);

        return $"INV-{year}-{count + 1:0000}";
    }

    private async Task<string> GenerateUniqueSharedCodeAsync()
    {
        while (true)
        {
            var sharedCode = $"CONF-{Guid.NewGuid().ToString("N")[..6].ToUpperInvariant()}";
            var exists = await _invoiceRepository.SharedCodeExistsAsync(sharedCode);

            if (!exists)
            {
                return sharedCode;
            }
        }
    }

    private static decimal ConvertToDecimal(float value)
    {
        return Convert.ToDecimal(value);
    }

    private static string? NormalizeText(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        return value.Trim();
    }

    private static string NormalizeRequiredText(
        string? value,
        string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new Exception(errorMessage);
        }

        return value.Trim();
    }
}