namespace Timesheet.Models;

public class TimesheetApprovalRequest
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public string ApprovalToken { get; set; } = string.Empty;

    public DateTime PeriodStart { get; set; }

    public DateTime PeriodEnd { get; set; }

    public string Status { get; set; } = "Pending";

    public string SentToEmail { get; set; } = string.Empty;

    public DateTime? ApprovedAt { get; set; }

    public DateTime? RejectedAt { get; set; }

    public string ClientComment { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Client? Client { get; set; }
}
