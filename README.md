# CRM OrionTek Project

## Requirements
- .NET 6.0 or higher
- Node.js 16 or higher
- Angular CLI
- SQL Server

## Backend Setup
1. Open the solution in Visual Studio or a compatible IDE.
2. Configure the connection string in `appsettings.Development.json`.
3. Run `Update-Database` in the Package Manager Console to apply migrations.
4. Start the project using `dotnet run`.

## Frontend Setup
1. Navigate to the Angular project folder in the terminal.
2. Run `npm install` to install dependencies.
3. Configure the `environment.development.ts` file with the correct API URL of the backend.
4. Run `ng serve` to start the Angular application.

## Features
- Client Management: Create, edit, delete, and list clients.
- Location Management: Create, edit, delete, and list locations associated with clients.

## Notes
- Ensure the backend is running before starting the frontend.
- If you encounter any issues, check the configured error handling and logs.