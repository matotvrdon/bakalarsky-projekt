using Microsoft.Extensions.Hosting;
using Web.Services.Abstractions;
using Web.Services.DTOs.Invoice;

using iText.Kernel.Pdf;
using iText.Forms;
using iText.Forms.Fields;

namespace Web.Services.Services;

public class PdfService : IPdfService
{
    private readonly IHostEnvironment _env;

    public PdfService(IHostEnvironment env)
    {
        _env = env;
    }

    public Task<byte[]> GenerateInvoicePdfAsync(InvoiceDto invoiceDto)
    {
        if (invoiceDto is null)
            throw new ArgumentNullException(nameof(invoiceDto));

        var templatePath = Path.Combine(_env.ContentRootPath, "Resources", "PDF", "Faktura.pdf");
        
        if (!File.Exists(templatePath))
            throw new FileNotFoundException("PDF template not found.", templatePath);
        
        using var ms = new MemoryStream();
        using (var pdf = new PdfDocument(new PdfReader(templatePath), new PdfWriter(ms)))
        {
            var form = PdfAcroForm.GetAcroForm(pdf, true);
            if (form == null)
                throw new InvalidOperationException("Failed to get PDF form.");
            
            var fields = form.GetAllFormFields();
            
            SetText(fields, "Text1", invoiceDto.IssueDate.ToString("dd.MM.yyyy"));
            SetText(fields, "Text2", invoiceDto.DueDate.ToString("dd.MM.yyyy"));
            
            // form.FlattenFields();
        }
        

        return Task.FromResult(ms.ToArray());
    }

    private static void SetText(IDictionary<string, PdfFormField> fields, string fieldName, string? value)
    {
        if (!fields.TryGetValue(fieldName, out var field))
            throw new InvalidOperationException($"PDF field '{fieldName}' not found.");
        
        field.SetValue(value ?? string.Empty);
    }
}