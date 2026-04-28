using Web.Domain.Models;

namespace Web.Services.Abstractions;

public interface IInvoicePdfStorageService
{
    Task<FileManager> StoreOrReplaceAsync(Invoice invoice);
}