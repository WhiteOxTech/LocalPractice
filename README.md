# Shop Management System

A lightweight ASP.NET Core Razor Pages application for managing transactions in small computer/Xerox shops.

## Features

- **Transaction Management**: Record and track service transactions (Xerox, Printing, Transfer, etc.)
- **Admin Dashboard**: View monthly summary of Total Amount In, Amount Out, and Service Charges
- **Staff Access**: Simple form to enter new transactions
- **Cookie-Based Authentication**: Simple, hardcoded credentials for Admin and Staff roles
- **SQLite Database**: Lightweight database perfect for low-spec systems
- **Bootstrap UI**: Clean, responsive interface
- **Role-Based Authorization**: Admin and Staff roles with different access levels

## Technology Stack

- **Framework**: ASP.NET Core 8.0 (Razor Pages)
- **ORM**: Entity Framework Core
- **Database**: SQLite
- **Authentication**: Cookie-based with custom authorization
- **UI**: Bootstrap 5.3
- **Language**: C# 12

## Project Structure

```
ShopManagementSystem/
├── Models/
│   └── Transaction.cs           # Transaction data model
├── Data/
│   └── ShopDbContext.cs         # EF Core DbContext
├── Pages/
│   ├── Index.cshtml             # Welcome page
│   ├── Login.cshtml             # Login page
│   ├── Login.cshtml.cs          # Login logic
│   ├── Logout.cshtml.cs         # Logout logic
│   ├── Admin/
│   │   ├── Dashboard.cshtml     # Admin dashboard with monthly summary
│   │   └── Dashboard.cshtml.cs  # Dashboard logic
│   └── Staff/
│       ├── NewTransaction.cshtml # Transaction entry form
│       └── NewTransaction.cshtml.cs # Transaction creation logic
├── wwwroot/
│   ├── css/                     # CSS files
│   └── js/                      # JavaScript files
├── Program.cs                   # Application configuration
├── appsettings.json            # Configuration
├── appsettings.Development.json # Development settings
└── ShopManagementSystem.csproj  # Project file
```

## Getting Started

### Prerequisites

- .NET 8.0 or later
- Windows, Linux, or macOS

### Installation

1. Clone or download the project
2. Navigate to the project directory:
   ```bash
   cd ShopManagementSystem
   ```

3. Restore dependencies:
   ```bash
   dotnet restore
   ```

4. Build the project:
   ```bash
   dotnet build
   ```

### Running the Application

```bash
dotnet run
```

The application will start at `https://localhost:5001` (or another available port).

### Default Credentials

For demonstration purposes, the following hardcoded credentials are available:

**Admin Account:**
- Username: `admin`
- Password: `admin123`
- Access: Dashboard, Analytics

**Staff Account:**
- Username: `staff`
- Password: `staff123`
- Access: Transaction entry form only

> **Note**: For production use, replace hardcoded authentication with a proper authentication mechanism (e.g., Azure AD, Gmail OAuth, or a dedicated identity provider).

## Transaction Model

The `Transaction` entity contains:

- **Id**: Unique identifier
- **Timestamp**: Date and time of transaction
- **ServiceType**: Type of service (Xerox, Printing, Transfer, etc.)
- **AmountIn**: Money received
- **AmountOut**: Money spent
- **ServiceCharge**: Profit or service charge
- **CreatedBy**: Username of the person who created the record

## Admin Dashboard

The Admin Dashboard provides:

- **Monthly Summary**: Total Amount In, Amount Out, and Service Charge
- **Transaction List**: Detailed view of all transactions for the selected month
- **Filtering**: Filter by service type and month
- **Real-time Calculations**: Automatic calculation of totals

## Staff Transaction Form

The Staff form allows:

- Selection of service type
- Entry of amount in and out
- Entry of service charge
- Automatic timestamp and user tracking

## Database

The application uses SQLite for data storage. The database file (`shop.db`) is automatically created in the project root on first run.

### Database Schema

**Transactions Table:**
```sql
CREATE TABLE Transactions (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Timestamp DATETIME NOT NULL,
    ServiceType VARCHAR(100) NOT NULL,
    AmountIn DECIMAL(10, 2) NOT NULL,
    AmountOut DECIMAL(10, 2) NOT NULL,
    ServiceCharge DECIMAL(10, 2) NOT NULL,
    CreatedBy VARCHAR(100) NOT NULL
);
```

## Configuration

Edit `appsettings.json` to customize:

- **Connection String**: Modify the SQLite database path
- **Logging Levels**: Adjust logging verbosity

### Example `appsettings.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=shop.db"
  }
}
```

## Performance Optimization

The system is optimized for low-spec computers:

- **Minimal Dependencies**: Only essential NuGet packages
- **SQLite**: Lightweight database, no server required
- **Efficient Queries**: LINQ to Entities with proper filtering
- **Bootstrap CDN**: No heavy frontend frameworks
- **Small Payload**: Razor Pages render minimal HTML

## Future Enhancements

- [ ] Google/Gmail OAuth authentication
- [ ] Multi-language support
- [ ] Advanced reporting and analytics
- [ ] Backup and restore functionality
- [ ] Mobile-responsive improvements
- [ ] Export to Excel/PDF

## Troubleshooting

### Database not found
Ensure the `shop.db` file is created automatically or manually create it by running migrations.

### Port already in use
Modify the port in `launchSettings.json` or use:
```bash
dotnet run --urls "https://localhost:5002"
```

### Authentication issues
Verify that cookies are enabled in your browser.

## Support

For issues, questions, or suggestions, please refer to the project documentation or contact the development team.

## License

This project is provided as-is for use in small retail/service shops.

---

**Version**: 1.0  
**Last Updated**: March 15, 2026
