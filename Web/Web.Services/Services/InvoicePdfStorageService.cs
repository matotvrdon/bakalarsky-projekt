using AutoMapper;
using Web.DataAccess.Abstractions;
using Web.Domain.Enums;
using Web.Domain.Models;
using Web.Services.Abstractions;

namespace Web.Services.Services;

public class InvoicePdfStorageService : IInvoicePdfStorageService
{
    private readonly IInvoicePdfGenerator _invoicePdfGenerator;
    private readonly IFileManagerService _fileManagerService;
    private readonly IFileManagerRepository _fileManagerRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IMapper _mapper;

    public InvoicePdfStorageService(
        IInvoicePdfGenerator invoicePdfGenerator,
        IFileManagerService fileManagerService,
        IFileManagerRepository fileManagerRepository,
        IInvoiceRepository invoiceRepository,
        IMapper mapper)
    {
        _invoicePdfGenerator = invoicePdfGenerator;
        _fileManagerService = fileManagerService;
        _fileManagerRepository = fileManagerRepository;
        _invoiceRepository = invoiceRepository;
        _mapper = mapper;
    }

    public async Task<FileManager> StoreOrReplaceAsync(Invoice invoice)
    {
        var ownerParticipantId = GetOwnerParticipantId(invoice);
        var pdfBytes = await _invoicePdfGenerator.GeneratePdfAsync(invoice.Id);

        var fileName = $"{invoice.InvoiceNumber}.pdf";

        var fileManagerDto = await _fileManagerService.StoreGeneratedFileAsync(
            pdfBytes,
            ownerParticipantId,
            FileType.Invoice,
            fileName,
            fileName
        );

        var fileManager = await _fileManagerRepository.GetFileManagerByIdAsync(fileManagerDto.Id);

        if (fileManager == null)
        {
            throw new Exception("Generated invoice FileManager record could not be loaded.");
        }

        invoice.FileManagerId = fileManager.Id;
        await _invoiceRepository.UpdateAsync(invoice);

        return fileManager;
    }

    private static int GetOwnerParticipantId(Invoice invoice)
    {
        var participantId = invoice.Participants
            .OrderBy(participant => participant.Id)
            .Select(participant => participant.ParticipantId)
            .FirstOrDefault();

        if (participantId == 0)
        {
            throw new Exception("Invoice does not have any participant.");
        }

        return participantId;
    }
}