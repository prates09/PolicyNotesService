PolicyNotesService – Microservice with .NET 8, EF Core InMemory & xUnit Testing

A small microservice to store and retrieve internal notes for insurance policy numbers.
Built as part of the assignment using .NET 8 Minimal APIs, EF Core InMemory, Repository + Service layers, and Unit & Integration Testing using xUnit.

Features

.NET 8 Minimal API

EF Core InMemory Database

Repository Layer

Service Layer

Unit Tests (xUnit)

Integration Tests (xUnit + WebApplicationFactory)

Swagger UI for API testing

Project Structure
PolicyNotesService.sln
│
├── PolicyNotesService
│   ├── appsettings.Development.json
│   ├── appsettings.json
│   ├── PolicyNotesService.csproj
│   ├── PolicyNotesService.http
│   ├── Program.cs
│   │
│   ├── Data
│   │   └── PolicyNotesDbContext.cs
│   │
│   ├── Models
│   │   └── PolicyNote.cs
│   │
│   ├── Properties
│   │   └── launchSettings.json
│   │
│   ├── Repositories
│   │   ├── IPolicyNoteRepository.cs
│   │   └── PolicyNoteRepository.cs
│   │
│   └── Services
│       ├── IPolicyNotesService.cs
│       └── PolicyNotesService.cs
│
└── PolicyNotesService.Tests
    ├── PolicyNotesService.Tests.csproj
    │
    ├── IntegrationTests
    │   └── NotesEndpointsIntegrationTests.cs
    │
    └── UnitTests
        └── PolicyNotesServiceUnitTests.cs

How to Run
1. Navigate to project folder:
cd PolicyNotesService

2. Run the API:
dotnet run

3. Open Swagger UI:
https://localhost:7096/swagger

API Endpoints
POST /notes

Creates a new note.
Example request body:

{
  "policyNumber": "POL-123",
  "note": "Swagger test note"
}

GET /notes

Retrieves all notes.

GET /notes/{id}

Retrieves a note by ID.

200 OK → Found

404 NotFound → Missing

Testing
Unit Tests (xUnit)

File:

PolicyNotesService.Tests/UnitTests/PolicyNotesServiceUnitTests.cs


Covers:

Adding a note

Retrieving notes

Integration Tests (xUnit)

File:

PolicyNotesService.Tests/IntegrationTests/NotesEndpointsIntegrationTests.cs


Covers:

POST /notes → 201 Created

GET /notes → 200 OK

GET /notes/{id} → 200 OK when found

GET /notes/{id} → 404 NotFound when missing

The tests override the API’s database service and inject a test-only InMemory database:

services.AddDbContext<PolicyNotesDbContext>(options =>
    options.UseInMemoryDatabase("IntegrationTestDb"));

Screenshots Required for Assignment

Project structure (command: tree /f)

Test Explorer (all tests passing)

Swagger UI showing endpoints and responses

Tech Stack

.NET 8

ASP.NET Core Minimal API

EF Core InMemory

xUnit

WebApplicationFactory

Swagger / Swashbuckle

Conclusion

This project fully satisfies the assignment requirements by implementing a clean, testable, and lightweight microservice with proper separation of concerns and complete automated testing.
