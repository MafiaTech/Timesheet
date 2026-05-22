using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Timesheet.Models;

namespace Timesheet.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<TimesheetEntry> TimesheetEntries { get; set; }

    public DbSet<Invoice> Invoices { get; set; }

    public DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }

    public DbSet<CompanyProfile> CompanyProfiles { get; set; }

    public DbSet<TimesheetApprovalRequest> TimesheetApprovalRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Project>()
            .HasOne(project => project.Client)
            .WithMany()
            .HasForeignKey(project => project.ClientId);

        modelBuilder.Entity<TimesheetEntry>()
            .HasOne(entry => entry.Project)
            .WithMany()
            .HasForeignKey(entry => entry.ProjectId);
            

        modelBuilder.Entity<Invoice>()
            .HasOne(invoice => invoice.Client)
            .WithMany()
            .HasForeignKey(invoice => invoice.ClientId);

        modelBuilder.Entity<InvoiceLineItem>()
            .HasOne(lineItem => lineItem.Invoice)
            .WithMany()
            .HasForeignKey(lineItem => lineItem.InvoiceId);

        modelBuilder.Entity<InvoiceLineItem>()
            .HasOne(lineItem => lineItem.TimesheetEntry)
            .WithMany()
            .HasForeignKey(lineItem => lineItem.TimesheetEntryId);

        modelBuilder.Entity<TimesheetApprovalRequest>()
            .HasOne(request => request.Client)
            .WithMany()
            .HasForeignKey(request => request.ClientId);
        
        modelBuilder.Entity<TimesheetEntry>()
            .HasOne(entry => entry.TimesheetApprovalRequest)
            .WithMany()
            .HasForeignKey(entry => entry.TimesheetApprovalRequestId);

        modelBuilder.Entity<ApplicationUser>()
            .HasOne(user => user.Client)
            .WithMany()
            .HasForeignKey(user => user.ClientId);
    }
}
