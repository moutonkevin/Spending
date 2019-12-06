# Prerequisites

Install Microsoft.EntityFrameworkCore.Tools to be able to use the Package Manager Console in VS

# Create a migration

Add-Migration InitialCreate

# Apply migrations

Update-Database

# Secrets

## Init project

dotnet user-secrets init

## Add secret

dotnet user-secrets set "ConnectionStrings:SpendingDatabase" "Data Source=localhost\SQLEXPRESS01;Database=spending-db;Trusted_Connection=True;"

## See secrets

Right click on the project then "Manage User Secrets"



