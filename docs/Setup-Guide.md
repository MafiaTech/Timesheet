# Setup Guide

This guide explains how to run Side Hustle Timesheet locally for development and testing.

## Prerequisites

- .NET 10 SDK
- Docker Desktop
- Git
- A terminal such as PowerShell

## Start Docker

Open Docker Desktop and wait until it reports that Docker is running.

## Run Docker Compose

From the project root, start the PostgreSQL and pgAdmin services:

```powershell
docker compose up -d
```

Verify that the containers are running:

```powershell
docker compose ps
```

The compose file starts:

- PostgreSQL on port `5432`
- pgAdmin on port `5050`

## Build the App

From the project root, run:

```powershell
dotnet build
```

## Run the App

Start the application:

```powershell
dotnet run
```

Open the local URL printed in the terminal.

## Login

Use the default admin account:

- Email: `admin@gmtech.co.za`
- Password: `Admin@123`

If seeded client data exists for Mopani, the test client account is:

- Email: `client@mopani.co.za`
- Password: `Client@123`

