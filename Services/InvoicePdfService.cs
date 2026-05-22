using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Timesheet.Models;

namespace Timesheet.Services;

public class InvoicePdfService
{
    public byte[] GenerateInvoicePdf(Invoice invoice, List<InvoiceLineItem> lineItems, CompanyProfile? companyProfile)
    {
        var culture = new CultureInfo("en-ZA");
        var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "gm-technologies-logo.png");
        var logoExists = File.Exists(logoPath);

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(text => text.FontSize(10));

                page.Header().Column(column =>
                {
                    if (logoExists)
                    {
                        column.Item()
                            .Width(120)
                            .Image(logoPath);
                    }

                    if (companyProfile is null)
                    {
                        column.Item().PaddingTop(10).Text("Company Profile Not Configured")
                            .FontSize(14)
                            .Bold()
                            .FontColor(Colors.Red.Darken2);
                    }
                    else
                    {
                        column.Item().PaddingTop(10).Text(companyProfile.CompanyName)
                            .FontSize(16)
                            .Bold();

                        column.Item().Text($"Contact: {companyProfile.ContactPerson}");
                        column.Item().Text($"Email: {companyProfile.Email}");
                        column.Item().Text($"Phone: {companyProfile.Phone}");
                        column.Item().Text(companyProfile.Address);

                        if (!string.IsNullOrWhiteSpace(companyProfile.VatNumber))
                        {
                            column.Item().Text($"VAT Number: {companyProfile.VatNumber}");
                        }
                    }

                    column.Item()
                        .PaddingTop(20)
                        .Text("Invoice")
                        .FontSize(24)
                        .Bold();

                    column.Item().PaddingTop(10).Row(row =>
                    {
                        row.RelativeItem().Column(details =>
                        {
                            details.Item().Text($"Invoice Number: {invoice.InvoiceNumber}");
                            details.Item().Text($"Invoice Date: {invoice.InvoiceDate.ToString("yyyy-MM-dd", culture)}");
                            details.Item().Text($"Client Name: {invoice.Client?.Name}");
                        });

                        row.RelativeItem().Column(details =>
                        {
                            details.Item().Text($"Period Start: {invoice.PeriodStart.ToString("yyyy-MM-dd", culture)}");
                            details.Item().Text($"Period End: {invoice.PeriodEnd.ToString("yyyy-MM-dd", culture)}");
                        });
                    });
                });

                page.Content().PaddingVertical(25).Column(column =>
                {
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(75);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(3);
                            columns.ConstantColumn(55);
                            columns.ConstantColumn(70);
                            columns.ConstantColumn(75);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(HeaderCell).Text("Work Date");
                            header.Cell().Element(HeaderCell).Text("Project");
                            header.Cell().Element(HeaderCell).Text("Activity");
                            header.Cell().Element(HeaderCell).AlignRight().Text("Hours");
                            header.Cell().Element(HeaderCell).AlignRight().Text("Rate");
                            header.Cell().Element(HeaderCell).AlignRight().Text("Amount");
                        });

                        foreach (var lineItem in lineItems)
                        {
                            table.Cell().Element(BodyCell).Text(lineItem.WorkDate.ToString("yyyy-MM-dd", culture));
                            table.Cell().Element(BodyCell).Text(lineItem.ProjectName);
                            table.Cell().Element(BodyCell).Text(lineItem.ActivityDescription);
                            table.Cell().Element(BodyCell).AlignRight().Text(lineItem.HoursWorked.ToString("N2", culture));
                            table.Cell().Element(BodyCell).AlignRight().Text(lineItem.HourlyRate.ToString("C", culture));
                            table.Cell().Element(BodyCell).AlignRight().Text(lineItem.Amount.ToString("C", culture));
                        }
                    });

                    column.Item().PaddingTop(20).AlignRight().Column(totals =>
                    {
                        totals.Item().Text($"Total Hours: {invoice.TotalHours.ToString("N2", culture)}").Bold();
                        totals.Item().Text($"Total Amount: {invoice.TotalAmount.ToString("C", culture)}").Bold();
                    });

                    if (companyProfile is not null)
                    {
                        column.Item().PaddingTop(25).Column(banking =>
                        {
                            banking.Item().Text("Banking Details").Bold();
                            banking.Item().Text($"Bank Name: {companyProfile.BankName}");
                            banking.Item().Text($"Account Number: {companyProfile.AccountNumber}");
                            banking.Item().Text($"Branch Code: {companyProfile.BranchCode}");
                        });

                        if (!string.IsNullOrWhiteSpace(companyProfile.InvoiceFooterMessage))
                        {
                            column.Item().PaddingTop(20).Text(companyProfile.InvoiceFooterMessage);
                        }
                    }
                });

                page.Footer()
                    .AlignCenter()
                    .Text("Generated by Side Hustle Timesheet")
                    .FontSize(9)
                    .FontColor(Colors.Grey.Darken1);
            });
        }).GeneratePdf();
    }

    private static IContainer HeaderCell(IContainer container)
    {
        return container
            .Background(Colors.Grey.Lighten3)
            .Border(1)
            .BorderColor(Colors.Grey.Lighten1)
            .Padding(5);
    }

    private static IContainer BodyCell(IContainer container)
    {
        return container
            .BorderBottom(1)
            .BorderColor(Colors.Grey.Lighten2)
            .Padding(5);
    }
}
