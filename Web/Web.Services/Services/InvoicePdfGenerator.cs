using System.Globalization;
using AutoMapper;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Web.DataAccess.Abstractions;
using Web.Domain.Enums;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Services.Services;

public class InvoicePdfGenerator : IInvoicePdfGenerator
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;

    public InvoicePdfGenerator(
        IInvoiceRepository invoiceRepository,
        ISupplierRepository supplierRepository,
        IMapper mapper)
    {
        _invoiceRepository = invoiceRepository;
        _supplierRepository = supplierRepository;
        _mapper = mapper;
    }

    public async Task<byte[]> GeneratePdfAsync(int invoiceId)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);

        if (invoice == null)
        {
            throw new Exception($"Invoice with id {invoiceId} not found.");
        }

        var invoiceDto = _mapper.Map<InvoiceDto>(invoice);

        var supplier = await _supplierRepository.GetAsync();

        if (supplier == null)
        {
            throw new Exception("Supplier was not found.");
        }

        using var ms = new MemoryStream();

        var document = Document.Create(document =>
        {
            document.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1.5f, Unit.Centimetre);
                page.DefaultTextStyle(text => text.FontSize(10));

                page.Header()
                    .Height(35)
                    .Border(1)
                    .PaddingHorizontal(10)
                    .AlignMiddle()
                    .Row(row =>
                    {
                        row.RelativeItem()
                            .Text("FAKTÚRA")
                            .FontSize(20)
                            .Bold();

                        row.RelativeItem()
                            .AlignRight()
                            .Text($"Číslo: {invoiceDto.InvoiceNumber}")
                            .FontSize(16)
                            .Bold();
                    });

                page.Content()
                    .Border(1)
                    .Column(column =>
                    {
                        column.Item().Row(row =>
                        {
                            row.RelativeItem()
                                .Border(1)
                                .MinHeight(250)
                                .Padding(10)
                                .Column(supplierColumn =>
                                {
                                    supplierColumn.Spacing(4);

                                    supplierColumn.Item()
                                        .Text("Dodávateľ")
                                        .FontSize(13)
                                        .Bold();

                                    WriteSupplierInfo(supplierColumn, supplier);
                                });

                            row.RelativeItem()
                                .Column(rightColumn =>
                                {
                                    rightColumn.Item()
                                        .Border(1)
                                        .MinHeight(150)
                                        .Padding(10)
                                        .Column(customerColumn =>
                                        {
                                            customerColumn.Spacing(4);

                                            customerColumn.Item()
                                                .Text("Odberateľ")
                                                .FontSize(13)
                                                .Bold();

                                            WriteCustomerInfo(customerColumn, invoiceDto);
                                        });

                                    rightColumn.Item()
                                        .Border(1)
                                        .MinHeight(100)
                                        .Table(table =>
                                        {
                                            table.ColumnsDefinition(columns =>
                                            {
                                                columns.RelativeColumn(1);
                                                columns.RelativeColumn(1);
                                            });

                                            WriteInfoRow(
                                                table,
                                                "Dátum vyhotovenia:",
                                                invoiceDto.CreatedAtUtc.ToString("dd.MM.yyyy")
                                            );

                                            WriteInfoRow(
                                                table,
                                                "Dátum splatnosti:",
                                                invoiceDto.DueDateUtc.ToString("dd.MM.yyyy")
                                            );

                                            WriteInfoRow(
                                                table,
                                                "Variabilný symbol:",
                                                GetVariableSymbol(invoiceDto.InvoiceNumber)
                                            );

                                            WriteInfoRow(
                                                table,
                                                "Forma úhrady:",
                                                "Bankový prevod"
                                            );

                                            WriteInfoRow(
                                                table,
                                                "Konštantný symbol:",
                                                "0308"
                                            );
                                        });
                                });
                        });

                        column.Item()
                            .Border(1)
                            .Padding(10)
                            .Text("Na základe registrácie na konferenciu Vám fakturujeme:")
                            .Bold();

                        column.Item()
                            .Border(1)
                            .Padding(10)
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(1.2f);
                                    columns.RelativeColumn(9);
                                    columns.RelativeColumn(1.6f);
                                    columns.RelativeColumn(2.5f);
                                    columns.RelativeColumn(2.2f);
                                    columns.RelativeColumn(2.8f);
                                });

                                table.Header(header =>
                                {
                                    WriteHeaderCell(header.Cell(), "P. č.");
                                    WriteHeaderCell(header.Cell(), "Popis");
                                    WriteHeaderCell(header.Cell(), "MJ");
                                    WriteHeaderCell(header.Cell(), "Cena za j.");
                                    WriteHeaderCell(header.Cell(), "Množstvo");
                                    WriteHeaderCell(header.Cell(), "Spolu");
                                });

                                var itemNumber = 1;

                                foreach (var item in invoiceDto.Items.OrderBy(item => item.Id))
                                {
                                    var participantName = GetParticipantName(invoiceDto, item.ParticipantId);
                                    var itemDescription = string.IsNullOrWhiteSpace(participantName)
                                        ? item.Name
                                        : $"{participantName} - {item.Name}";

                                    WriteBodyCell(table, itemNumber.ToString(), true);
                                    WriteBodyCell(table, itemDescription, false);
                                    WriteBodyCell(table, "ks", true);
                                    WriteBodyCell(table, FormatMoney(item.UnitPrice), true);
                                    WriteBodyCell(table, item.Quantity.ToString(CultureInfo.InvariantCulture), true);
                                    WriteBodyCell(table, FormatMoney(item.TotalPrice), true);

                                    itemNumber++;
                                }
                            });

                        column.Item()
                            .Padding(10)
                            .AlignRight()
                            .Text($"Spolu = {FormatMoney(invoiceDto.TotalAmount)} EUR")
                            .FontSize(14)
                            .Bold();

                        column.Item()
                            .PaddingHorizontal(10)
                            .PaddingBottom(10)
                            .Column(paymentColumn =>
                            {
                                paymentColumn.Spacing(3);

                                paymentColumn.Item()
                                    .Text("Platobné údaje")
                                    .Bold();

                                if (!string.IsNullOrWhiteSpace(supplier.Iban))
                                {
                                    paymentColumn.Item().Text(text =>
                                    {
                                        text.Span("IBAN: ").Bold();
                                        text.Span(supplier.Iban);
                                    });
                                }

                                if (!string.IsNullOrWhiteSpace(supplier.Swift))
                                {
                                    paymentColumn.Item().Text(text =>
                                    {
                                        text.Span("SWIFT: ").Bold();
                                        text.Span(supplier.Swift);
                                    });
                                }

                                paymentColumn.Item().Text(text =>
                                {
                                    text.Span("Variabilný symbol: ").Bold();
                                    text.Span(GetVariableSymbol(invoiceDto.InvoiceNumber));
                                });
                            });
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Strana ");
                        text.CurrentPageNumber();
                        text.Span(" / ");
                        text.TotalPages();
                    });
            });
        });

        document.GeneratePdf(ms);

        return ms.ToArray();
    }

    private static void WriteSupplierInfo(ColumnDescriptor column, Supplier supplier)
    {
        WriteTextLine(column, "Názov:", supplier.Name);
        WriteTextLine(column, "Ulica:", supplier.Street);

        if (!string.IsNullOrWhiteSpace(supplier.PostalCode) ||
            !string.IsNullOrWhiteSpace(supplier.City))
        {
            WriteTextLine(
                column,
                "Mesto:",
                $"{supplier.PostalCode} {supplier.City}".Trim()
            );
        }

        WriteTextLine(column, "Štát:", supplier.Country);
        WriteTextLine(column, "IČO:", supplier.Ico);
        WriteTextLine(column, "DIČ:", supplier.Dic);
        WriteTextLine(column, "IČ DPH:", supplier.IcDph);
        WriteTextLine(column, "Banka:", supplier.Bank);

        if (!string.IsNullOrWhiteSpace(supplier.Address) ||
            !string.IsNullOrWhiteSpace(supplier.AddressPostalCode) ||
            !string.IsNullOrWhiteSpace(supplier.AddressCity))
        {
            var address = string.Join(
                "\n",
                new[]
                {
                    supplier.Address,
                    $"{supplier.AddressPostalCode} {supplier.AddressCity}".Trim()
                }.Where(value => !string.IsNullOrWhiteSpace(value))
            );

            WriteTextLine(column, "Adresa:", address);
        }

        WriteTextLine(column, "Číslo účtu:", supplier.BankAccount);
        WriteTextLine(column, "SWIFT:", supplier.Swift);
        WriteTextLine(column, "IBAN:", supplier.Iban);
        WriteTextLine(column, "Telefón:", supplier.Phone);
    }

    private static void WriteCustomerInfo(ColumnDescriptor column, InvoiceDto invoice)
    {
        var customerName = invoice.CustomerType == InvoiceCustomerType.Company
            ? invoice.CompanyName
            : invoice.CustomerName;

        WriteTextLine(column, "Odberateľ:", customerName);
        WriteTextLine(column, "Adresa:", invoice.BillingAddress);

        if (invoice.CustomerType == InvoiceCustomerType.Company)
        {
            WriteTextLine(column, "IČO:", invoice.Ico);
            WriteTextLine(column, "DIČ:", invoice.Dic);
            WriteTextLine(column, "IČ DPH:", invoice.VatId);
        }
    }

    private static void WriteTextLine(
        ColumnDescriptor column,
        string label,
        string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        column.Item().Text(text =>
        {
            text.Span(label + " ").Bold();
            text.Span(value);
        });
    }

    private static void WriteInfoRow(
        TableDescriptor table,
        string label,
        string value)
    {
        table.Cell()
            .Border(1)
            .PaddingVertical(4)
            .PaddingHorizontal(4)
            .Text(label);

        table.Cell()
            .Border(1)
            .PaddingVertical(4)
            .PaddingHorizontal(4)
            .AlignRight()
            .Text(value);
    }

    private static void WriteHeaderCell(IContainer container, string text)
    {
        container
            .Border(1)
            .Background(Colors.Grey.Lighten3)
            .AlignCenter()
            .Padding(3)
            .Text(text)
            .Bold();
    }

    private static void WriteBodyCell(
        TableDescriptor table,
        string text,
        bool alignCenterOrRight)
    {
        var cell = table.Cell()
            .Border(1)
            .Padding(3);

        if (alignCenterOrRight)
        {
            cell.AlignRight().Text(text);
            return;
        }

        cell.Text(text);
    }

    private static string FormatMoney(decimal value)
    {
        return value.ToString("F2", CultureInfo.InvariantCulture);
    }

    private static string GetParticipantName(
        InvoiceDto invoice,
        int? participantId)
    {
        if (!participantId.HasValue)
        {
            return string.Empty;
        }

        var participant = invoice.Participants
            .FirstOrDefault(item => item.ParticipantId == participantId.Value);

        return participant?.FullName ?? string.Empty;
    }

    private static string GetVariableSymbol(string invoiceNumber)
    {
        var digits = new string(invoiceNumber.Where(char.IsDigit).ToArray());

        if (string.IsNullOrWhiteSpace(digits))
        {
            return invoiceNumber;
        }

        return digits;
    }
}