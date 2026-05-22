namespace Timesheet.Models;

public class TimesheetEntry
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public DateTime WorkDate { get; set; }

    public string Workstream { get; set; } = string.Empty;

    public string ActivityDescription { get; set; } = string.Empty;

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public decimal HoursWorked { get; set; }

    public bool IsBillable { get; set; } = true;

    public decimal HourlyRate { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; } = string.Empty;

    public string InvoiceNumber { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Project Project { get; set; } = null!;

    public int? TimesheetApprovalRequestId { get; set; }

    public TimesheetApprovalRequest? TimesheetApprovalRequest { get; set; }
}