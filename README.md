# 📘 Policy Notes Service – Minimal API + InMemory EF Core + xUnit Tests

## 🚀 Overview
This project implements a small microservice **PolicyNotesService** that allows an insurance company to store and retrieve **internal notes** for customer policies.

It is developed according to the assignment requirements:

- **.NET 8 Minimal APIs**
- **Entity Framework Core InMemory Database**
- **Repository + Service Layers**
- **Unit Tests + Integration Tests using xUnit**

---

## 🏗️ Project Structure
```
PolicyNotesService.sln
│
├── PolicyNotesService
│ ├── appsettings.Development.json
│ ├── appsettings.json
│ ├── PolicyNotesService.csproj
│ ├── PolicyNotesService.http
│ ├── Program.cs
│ │
│ ├── Data
│ │ └── PolicyNotesDbContext.cs
│ │
│ ├── Models
│ │ └── PolicyNote.cs
│ │
│ ├── Properties
│ │ └── launchSettings.json
│ │
│ ├── Repositories
│ │ ├── IPolicyNoteRepository.cs
│ │ └── PolicyNoteRepository.cs
│ │
│ └── Services
│ ├── IPolicyNotesService.cs
│ └── PolicyNotesService.cs
│
└── PolicyNotesService.Tests
├── PolicyNotesService.Tests.csproj
│
├── IntegrationTests
│ └── NotesEndpointsIntegrationTests.cs
│
└── UnitTests
└── PolicyNotesServiceUnitTests.cs

```
---

## ⚙️ Features

### ✅ Add a Note  
POST `/notes` → Returns **201 Created**

### ✅ Retrieve All Notes  
GET `/notes` → Returns **200 OK**

### ✅ Retrieve Note by ID  
GET `/notes/{id}` →  
- **200 OK** when found  
- **404 NotFound** when missing  

### 🗄️ Database  
- **EF Core InMemory** used for API + tests  
- Data resets every time the application restarts  

---

## 🧪 Testing

### ✔ Unit Tests (xUnit)
Covers:
- Adding a policy note  
- Retrieving notes  

### ✔ Integration Tests (xUnit + WebApplicationFactory)
Covers:
- POST `/notes` → **201 Created**
- GET `/notes` → **200 OK**
- GET `/notes/{id}` → **200 / 404**, depending on existence  

---

## ▶️ How to Run the Project

### 1️⃣ Restore dependencies  
```bash
dotnet restore
2️⃣ Run the API
bash
Copy code
dotnet run --project PolicyNotesService
The API will start at:

arduino
Copy code
https://localhost:7096/
3️⃣ Open Swagger UI
bash
Copy code
https://localhost:7096/swagger
🧪 Run All Tests
bash
Copy code
dotnet test
📸 Screenshots Included
The submitted ZIP contains:

Project structure (via tree /f)

Test Explorer screenshot

Swagger testing screenshots for POST/GET

🙌 Author
Built as part of the Chubb – .NET Microservices Assignment.

