# Commercial Backlog

## Client Read-Only Refinements

- Add filters for date range, project, status, and billable entries.
- Add invoice and timesheet detail pages.
- Improve empty states and loading states.
- Add export options for client-visible tables.

## Interactive Dashboards

- Add charts for hours by project, status, and period.
- Add invoice aging summaries.
- Add approved versus pending workload views.
- Add drill-down dashboard cards.

## Grafana Analytics

- Create Grafana dashboards for operational metrics.
- Track invoice totals, outstanding balances, and approval turnaround.
- Add database read-only reporting access.
- Add dashboard provisioning for repeatable environments.

## Email SMTP Production Setup

- Move SMTP settings to secure production configuration.
- Add environment-specific email credentials.
- Add delivery failure logging.
- Add branded email templates.

## Multi-Tenant Support

- Add tenant ownership to users, clients, projects, timesheets, invoices, and company profiles.
- Enforce tenant scoping across all queries.
- Add tenant administration screens.
- Support tenant-specific branding and settings.

## Subscription Billing

- Add subscription plans.
- Integrate payment provider billing.
- Track subscription status and renewal dates.
- Restrict features by plan.

## Audit Logs

- Record login, approval, invoice, and account-management events.
- Store actor, timestamp, entity type, and entity ID.
- Add admin audit log search and filtering.
- Protect audit records from modification.

## Backup and Restore

- Define PostgreSQL backup schedule.
- Add restore procedure documentation.
- Test restore workflows regularly.
- Store encrypted backups in durable storage.

## Deployment and Hosting

- Prepare production Docker or cloud deployment configuration.
- Add environment-specific app settings.
- Configure managed PostgreSQL.
- Add health checks, logging, and monitoring.

## Security Hardening

- Enforce HTTPS in production.
- Review password and lockout policies.
- Add CSRF and authentication flow checks.
- Protect secrets with environment variables or a secret manager.
- Add dependency scanning and update process.

