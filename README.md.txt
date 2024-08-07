# Customers Solution

## Overview
The Customers Solution is a project consisting of a Web UI built using a .NET Core 8 Web App with Razor pages and an API developed with .NET Core 8 Web API following the principles of Clean Architecture. The solution uses a local SQL Server database for data storage.

## Table of Contents
- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Database Setup](#database-setup)
- [Running the Application](#running-the-application)
- [Project Structure](#project-structure)
- [Technologies Used](#technologies-used)

## Getting Started
These instructions will help you set up and run the project on your local machine for development and testing purposes.

## Prerequisites
- .NET Core SDK 8.0 or later
- SQL Server (Express or Developer Edition is sufficient)
- Visual Studio 2022 or later (optional but recommended)
- Postman (optional, for API testing)

## Installation
1. **Clone the repository:**
   ```bash
   git clone https://github.com/anketdhoble/Customers.git
   cd customers-solution
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

## Database Setup
1. **Create the database:**
   - Open SQL Server Management Studio (SSMS) and connect to your local SQL Server instance.
   - Create a new database named `CustomersDb`.

2. **Update the connection string:**
   - Open `appsettings.json` in both the `Customers.Api` and `Customers.Web` projects.
   - Update the connection string to match your SQL Server instance and database name.
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=CustomersDb;User Id=your-username;Password=your-password;"
     }
     ```

3. **Apply Migrations:**
   - Navigate to the `Customers.Persistence` project directory and run the following command to apply the migrations and create the necessary database schema.
     ```bash
     dotnet ef database update --startup-project ..\Customers.Api
     ```

## Running the Application
1. **Run the API:**
   - Open a terminal, navigate to the `Customers.Api` project directory, and run the application.
     ```bash
     cd Customers.Api
     dotnet run
     ```

2. **Run the Web UI:**
   - Open another terminal, navigate to the `Customers.Web` project directory, and run the application.
     ```bash
     cd Customers.Web
     dotnet run
     ```

3. **Access the Application:**
   - Open a web browser and navigate to `https://localhost:7229/Customer` for the Web UI.
   - The API will be accessible at `https://localhost:7028/api/v1`.

## Project Structure
```plaintext
Customers Solution/
│
├── Customers.Api/               # .NET Core Web API
│   ├── Controllers/
│   ├── Mapper/
│   ├── Middleware/
│   ├── Models/
│   ├── appsettings.json
│   ├── Customers.Api.http
│   └── Program.cs
│
├── Customers.Application/       # Application layer
│   ├── Interfaces/
│   ├── Models/
│   ├── Services/
│   └── Installer.cs
│
├── Customers.Domain/            # Domain layer
│   ├── Entities/
│   └── Installer.cs
│
├── Customers.Persistence/       # Persistence layer
│   ├── Context/
│   ├── Mapper/
│   ├── Migrations/
│   ├── Repositories/
│   └── Installer.cs
│
├── Customers.Web/               # .NET Core Web App with Razor pages
│   ├── Models/
│   ├── Pages/
│   ├── wwwroot/
│   ├── appsettings.json
│   └── Program.cs
│
│
└── README.md
```

## Technologies Used
- **.NET Core 8**: Framework for building web applications and APIs.
- **Razor Pages**: Simplified web application framework for building web UIs.
- **Entity Framework Core**: ORM for database access.
- **SQL Server**: Relational database management system.
- **Clean Architecture**: Architectural pattern to separate concerns and organize code.
