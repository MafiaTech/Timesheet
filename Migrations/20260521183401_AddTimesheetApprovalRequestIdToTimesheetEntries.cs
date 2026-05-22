using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timesheet.Migrations
{
    /// <inheritdoc />
    public partial class AddTimesheetApprovalRequestIdToTimesheetEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimesheetApprovalRequestId",
                table: "TimesheetEntries",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetEntries_TimesheetApprovalRequestId",
                table: "TimesheetEntries",
                column: "TimesheetApprovalRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimesheetEntries_TimesheetApprovalRequests_TimesheetApprova~",
                table: "TimesheetEntries",
                column: "TimesheetApprovalRequestId",
                principalTable: "TimesheetApprovalRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimesheetEntries_TimesheetApprovalRequests_TimesheetApprova~",
                table: "TimesheetEntries");

            migrationBuilder.DropIndex(
                name: "IX_TimesheetEntries_TimesheetApprovalRequestId",
                table: "TimesheetEntries");

            migrationBuilder.DropColumn(
                name: "TimesheetApprovalRequestId",
                table: "TimesheetEntries");
        }
    }
}
