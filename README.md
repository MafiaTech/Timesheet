# Side Hustle Timesheet

Side Hustle Timesheet is a Blazor-based time tracking, approval, and invoicing MVP for managing client work from timesheet capture through PDF invoice generation.

## Key Features Completed

- ASP.NET Core Identity authentication with Admin and Client roles.
- Protected Admin pages for dashboards, clients, projects, timesheets, invoices, and company profile management.
- Read-only Client portal pages for dashboard, timesheets, invoices, and company profile.
- Client-linked users with client-scoped data access.
- Timesheet capture, approval workflow, and invoice creation.
- PDF invoice export.
- PostgreSQL database persistence with Entity Framework Core.
- Docker Compose setup for local PostgreSQL and pgAdmin.

## Technology Stack

- .NET 10
- Blazor Server with Interactive Server rendering
- ASP.NET Core Identity
- Entity Framework Core
- PostgreSQL with Npgsql
- QuestPDF
- MailKit
- ClosedXML
- Bootstrap
- Docker Compose

## How to Run Locally

1. Start Docker Desktop.
2. Start the database services:

```powershell
docker compose up -d
```

3. Build the app:

```powershell
dotnet build
```

4. Run the app:

```powershell
dotnet run
```

5. Open the local URL shown by `dotnet run` and sign in.

## Default Admin Login

- Email: `admin@gmtech.co.za`
- Password: `Admin@123`

## Docker Services Used

- `postgres`: PostgreSQL 16 database container named `timesheet-db`.
- `pgadmin`: pgAdmin container named `timesheet-pgadmin`, available on port `5050`.

