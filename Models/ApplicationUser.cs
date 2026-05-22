using Microsoft.AspNetCore.Identity;

namespace Timesheet.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    public int? ClientId { get; set; }

    public Client? Client { get; set; }
}
