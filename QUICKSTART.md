# 🚀 Quick Start Guide

## 30 Seconds to Running the App

### Prerequisites
- .NET 8.0 SDK installed ([Download](https://dotnet.microsoft.com/en-us/download/dotnet/8.0))

### Run In 3 Commands

```powershell
# 1. Navigate to project
cd D:\DevelopmentWork\ShopManagementSystem

# 2. Restore and build
dotnet restore

# 3. Run the app
dotnet run
```

### Access the App
🌐 **URL**: https://localhost:5001 or http://localhost:5000

### Default Credentials

| Role  | Username | Password   |
|-------|----------|-----------|
| Admin | `admin`  | `admin123` |
| Staff | `staff`  | `staff123` |

## What You Get

✅ **Admin Dashboard**
- Monthly transaction summary
- Total Amount In, Out & Service Charge
- Filterable transaction list

✅ **Staff Form**
- Quick transaction entry
- Service type selection
- Auto timestamp & user tracking

✅ **Database**
- SQLite (auto-created as `shop.db`)
- No server setup required

✅ **UI**
- Bootstrap responsive design
- Fast, lightweight interface

## Folder Structure

```
ShopManagementSystem/
├── Pages/                    # Web pages
│   ├── Admin/Dashboard      # Admin dashboard
│   ├── Staff/NewTransaction # Staff form
│   └── Login                # Login page
├── Models/Transaction.cs    # Data model
├── Data/ShopDbContext.cs   # Database context
├── Program.cs              # App startup
└── shop.db                 # Database (auto-created)
```

## Common Commands

```bash
# Run in watch mode (auto-reload on changes)
dotnet watch run

# Build only
dotnet build

# Run on different port
dotnet run --urls "https://localhost:5002"

# Publish for deployment
dotnet publish -c Release -o publish
```

## Troubleshooting

### Port Already in Use?
```bash
dotnet run --urls "https://localhost:5002;http://localhost:5003"
```

### Database Issues?
Delete `shop.db` file and restart app (it auto-creates it)

### Not Installed .NET SDK?
Download: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

---

ℹ️ See **SETUP.md** for detailed installation  
📖 See **README.md** for full documentation  
📋 See **PROJECT_SUMMARY.md** for architecture details
