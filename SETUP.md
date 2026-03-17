# Shop Management System - Setup Guide

Since .NET SDK is not currently installed on this system, follow these steps to get the application running:

## Prerequisites

### 1. Install .NET 8.0 SDK

Download and install .NET 8.0 SDK from: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

#### On Windows:
- Download the Windows installer (.exe)
- Run the installer and follow the installation wizard
- Restart your computer after installation
- Verify installation by opening PowerShell and running: `dotnet --version`

#### On Linux (Ubuntu/Debian):
```bash
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0
```

#### On macOS:
```bash
brew install dotnet
```

## Running the Application

### Step 1: Navigate to Project Directory
```bash
cd D:\DevelopmentWork\ShopManagementSystem
```

### Step 2: Restore Dependencies
```bash
dotnet restore
```

### Step 3: Build the Project
```bash
dotnet build
```

### Step 4: Run the Application

**For HTTP (Development):**
```bash
dotnet run --project ShopManagementSystem.csproj --configuration Development
```

**For HTTPS (Production-like):**
```bash
dotnet run --configuration Release
```

### Step 5: Access the Application

Once the application is running, open your web browser and navigate to:
- **HTTP**: `http://localhost:5000`
- **HTTPS**: `https://localhost:5001`

You should see the welcome page with "Shop Management System" and a "Get Started" button.

## Default Login Credentials

Use these credentials to test the application:

### Admin Account
- **Username**: `admin`
- **Password**: `admin123`
- **Access**: Full dashboard with analytics and transaction history

### Staff Account
- **Username**: `staff`
- **Password**: `staff123`
- **Access**: Transaction entry form only

## Project Structure

```
ShopManagementSystem/
├── Models/
│   └── Transaction.cs              # Data model
├── Data/
│   └── ShopDbContext.cs            # Entity Framework Core context
├── Pages/
│   ├── Index.cshtml                # Welcome page
│   ├── Login.cshtml & .cs          # Login page
│   ├── Logout.cshtml & .cs         # Logout page
│   ├── Error.cshtml & .cs          # Error handling
│   ├── NotFound.cshtml             # 404 page
│   ├── Admin/
│   │   ├── Dashboard.cshtml        # Admin dashboard
│   │   └── Dashboard.cshtml.cs     # Dashboard logic
│   └── Staff/
│       ├── NewTransaction.cshtml   # Transaction entry form
│       └── NewTransaction.cshtml.cs # Form processing
├── Properties/
│   └── launchSettings.json         # Launch configuration
├── wwwroot/                         # Static files (CSS, JS, images)
├── Program.cs                       # Application startup
├── appsettings.json                # Configuration
├── appsettings.Development.json    # Development settings
└── ShopManagementSystem.csproj     # Project file
```

## Database

- **Type**: SQLite
- **File**: `shop.db`
- **Location**: Project root directory
- **Auto-created**: The database is automatically created on first run

### Database Schema

The application creates a single table:

```sql
Transactions
├── Id (INTEGER, PRIMARY KEY)
├── Timestamp (DATETIME)
├── ServiceType (VARCHAR(100))
├── AmountIn (DECIMAL(10,2))
├── AmountOut (DECIMAL(10,2))
├── ServiceCharge (DECIMAL(10,2))
└── CreatedBy (VARCHAR(100))
```

## Features Overview

### Admin Dashboard
- View all transactions for the current month
- Filter transactions by service type
- Filter transactions by month
- See totals for:
  - Amount In (revenue)
  - Amount Out (expenses)
  - Service Charge (profit)

### Staff Transaction Form
- Create new transaction records
- Select service type (Xerox, Printing, Transfer)
- Enter amounts and service charges
- Automatic timestamp and user tracking

## Configuration

Edit `appsettings.json` to customize:

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

### Change Database Location

To use a different database path:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=path/to/your/database.db"
}
```

## Troubleshooting

### Port Already in Use
If port 5000 or 5001 is already in use:
```bash
dotnet run --urls "https://localhost:5002;http://localhost:5003"
```

### Database Lock Error
Delete `shop.db`, `shop.db-shm`, and `shop.db-wal` files from the project root and restart the application.

### Authentication Issues
- Clear browser cookies
- Close all browser tabs for the application
- Restart the application
- Try logging in again

### Dependency Issues
Run the following to clear and restore:
```bash
dotnet clean
dotnet restore
```

## Development Tips

### Run in Watch Mode
For automatic rebuild on file changes:
```bash
dotnet watch run
```

### View Database Contents
You can use any SQLite viewer:
- **Visual Studio Code**: Install "SQLite" extension
- **Online Tool**: https://sqliteonline.com
- **Command Line**: Download `sqlite3` command-line tool

### Code Structure Notes

- **AuthConstants.cs**: Centralized authentication configuration and helper methods
- **Program.cs**: Dependency injection, middleware configuration, and startup setup
- **Entity Framework**: LINQ queries in page models for data access
- **Razor Pages**: Separate code-behind files (`.cshtml.cs`) for business logic
- **Bootstrap CDN**: No local CSS/JS build required, ultra-lightweight

## Next Steps

### Future Enhancements
- [ ] Google/Gmail OAuth authentication
- [ ] Multi-user authentication with database
- [ ] Advanced reporting and analytics
- [ ] Backup and restore functionality
- [ ] Mobile app companion
- [ ] Invoice generation

### Production Deployment

For deploying to production:

1. **Azure App Service**:
   ```bash
   dotnet publish -c Release
   ```

2. **Docker**:
   - Create a Dockerfile
   - Build and push Docker image

3. **IIS**:
   - Install .NET Hosting Bundle
   - Publish as self-contained application

## Support

For issues or questions:
1. Check the project README.md
2. Review error messages in the console
3. Check browser developer tools (F12)
4. Verify database file exists and is not corrupted

## License

This project is provided as-is for educational and small business use.

---

**Version**: 1.0
**Last Updated**: March 2026
