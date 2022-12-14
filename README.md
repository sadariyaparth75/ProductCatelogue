# Practical Task

---

## Technologies Used

- .NET 6
- ASP.Net Core
  - MVC
    - For the whole product catalogue
  - Api
    - For the api part of the task
  - Razor Pages
    - Generated by Identity for Auth
    - I added phone number field for registration
- Entity Framework Core (EF Core)
  - Data access
- Asp.Net Core Identity
  - For Authentication
- SQLite
  - As the database
  - SQLite is a file based db
- If any new columns are added, run the following command to create a fresh migration:
  - `dotnet tool install --global dotnet-ef`
  - `dotnet ef migrations add MigrationNameHere`
  - `dotnet ef database update`

---

## Commands to run the project

- First download .NET 6 SDK from: <https://dotnet.microsoft.com/en-us/download/dotnet/6.0>
- Install the SDK
- Open a terminal in the project directory
- Run the following commands in order to install ef tool and update the database:
  - `dotnet tool install --global dotnet-ef`
  - `dotnet ef database update`
- Run the project with the following command:
  - `dotnet run`
