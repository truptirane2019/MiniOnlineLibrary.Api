A mini online library management system built with .NET Core Web API, Angular, Bootstrap, and SQL Server. The system supports Admin and User roles with book borrowing, returning, and overdue tracking.

** Backend Setup (.NET Core Web API)**

1) Clone the repository
git clone https://github.com/yourusername/mini-online-library.git
cd mini-online-library/backend

2) Install Dependencies
dotnet restore

Update Database Connection String

In appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=MiniLibraryDB;Trusted_Connection=True;"
}

Run Migrations & Update Database
dotnet ef migrations add InitialCreate
dotnet ef database update

Run the API
dotnet run


API runs on: https://localhost:5001 or http://localhost:5000
