namespace Timesheet.Models;

public class InvoiceLineItem
{
    public int Id { get; set; }

    public int InvoiceId { get; set; }

    public int TimesheetEntryId { get; set; }

    public DateTime WorkDate { get; set; }

    public string ProjectName { get; set; } = string.Empty;

    public string ActivityDescription { get; set; } = string.Empty;

    public string Workstream { get; set; } = string.Empty;

    public decimal HoursWorked { get; set; }

    public decimal HourlyRate { get; set; }

    public decimal Amount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Invoice? Invoice { get; set; }

    public TimesheetEntry? TimesheetEntry { get; set; }
}
