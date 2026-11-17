# Changelog

## (v0.2.0 in progress)

### Added
- `Category` model with seeded IT categories: Hardware, Software, Network, Account & Access, Email, Security, Service Request, Other
- `Department` model, seeded with the university colleges and offices that submit tickets
- `Ticket.CategoryId` (nullable) foreign key linking tickets to a category
- `UserEntity.DepartmentId` (nullable) foreign key linking users to a department
- Seed data through `HasData` in `AppDbContext.OnModelCreating`, applied via migration
- `LoginDto` and login endpoint (`POST /api/Auth/login`) that verifies the password against the stored hash and returns a generic error for both unknown email and wrong password
“No register or login pages in the frontend yet

### Changed
- “No register or login pages in the frontend yet
- `RegistrationDto` now has an optional `DepartmentId`, and `Username` and `Email` are required

### Migration
- Run `dotnet ef migrations add AddCategoryAndDepartment` then `dotnet ef database update` (stop `dotnet run` first)

### Known issues
- TODO(sec): Login returns plain user info and no token, so the frontend cannot stay logged in. JWT is the next step.
- TODO(sec): No rate limiting on login.
- TODO(sec): Register reveals which emails are already registered.
- React register form still sends `passwordHash`. It must send `password` or registration breaks.

## v0.1.0 - 2026-11-14
Initial release: working REST API for tickets with SQL Server.

### Added
- ASP.NET Core Web API project (.NET 10) with Models, Data, Controllers, DTOs, Validators and Migrations folders
- `Ticket` model with `TicketStatus` (Open, InProgress, Closed) and `PriorityLevel` (Low, Medium, High, Urgent)
- `AppDbContext` and EF Core (SQL Server) with the `InitialCreate` migration; `TicketTrackerDb` created via `dotnet ef database update`
- SQL Server running in Docker through `docker-compose.yml`
- `TicketsController`: GET list, GET by id, POST, DELETE
- Swagger UI for testing the API at /swagger (localhost:5144/swagger)
- User registration endpoint (`POST /api/Auth/register`) with password validation and hashed passwords
- Tickets linked to users through a `UserId` foreign key
- CORS policy allowing the React frontend (http://localhost:5173)
- `JsonStringEnumConverter` so status and priority return as text ("Open", "High") instead of numbers
- `.env` file loaded with DotNetEnv, so the connection string is no longer hardcoded in `appsettings.json`

### Fixed
- Package version mismatch: the latest EF Core packages target .NET 11, so SqlServer and Tools are pinned to 10.0.0
- Swashbuckle / Microsoft.OpenApi version conflict: pinned Swashbuckle.AspNetCore to 10.2.3
- `ISwaggerProvider` error: added `AddEndpointsApiExplorer` and `AddSwaggerGen` to Program.cs
- `dotnet ef` failing while `dotnet run` was still running (stop the API first)
- Foreign key error on ticket creation: a `UserId` that exists in the Users table is now required

### Known issues
- TODO(sec): POST /api/tickets accepts the full `Ticket` entity. Replace it with a `CreateTicketDto`.
- TODO(sec): The tickets endpoints have no authentication, and `UserId` comes from the client. Use JWT login in v0.2.
- TODO(sec): The register request field is named `passwordHash` but carries a plaintext password. Rename it to `password`.
- No update endpoint yet, so CRUD is incomplete.
- No tests or CI yet.

## Planned
- v0.2: Update endpoint, DTOs, login with JWT, unit tests, and frontend
- v0.3: To be decided