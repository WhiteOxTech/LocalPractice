# Project Summary - Shop Management System

## Overview

A lightweight ASP.NET Core Razor Pages application for managing transactions in small retail/service shops. Designed for low-spec computers with minimal dependencies.

- **Framework**: ASP.NET Core 8.0 (Razor Pages)
- **Database**: SQLite3
- **ORM**: Entity Framework Core
- **Authentication**: Cookie-based (hardcoded for demo)
- **UI Framework**: Bootstrap 5.3 (CDN)
- **Language**: C# 12

## All Project Files

### Configuration Files
| File | Purpose |
|------|---------|
| `ShopManagementSystem.csproj` | Project configuration, NuGet package references |
| `Program.cs` | Application startup, DI configuration, middleware setup |
| `appsettings.json` | Application settings, connection strings |
| `appsettings.Development.json` | Development-specific settings |
| `Properties/launchSettings.json` | Launch profiles (HTTP, HTTPS, IIS Express) |
| `.gitignore` | Git ignore patterns |

### Data & Models
| File | Purpose |
|------|---------|
| `Models/Transaction.cs` | Transaction entity with properties: Id, Timestamp, ServiceType, AmountIn, AmountOut, ServiceCharge, CreatedBy |
| `Data/ShopDbContext.cs` | EF Core DbContext configuration for SQLite |

### Core Application
| File | Purpose |
|------|---------|
| `AuthConstants.cs` | Hardcoded credentials and auth helper methods for Admin and Staff roles |

### Razor Pages - Authentication
| File | Purpose |
|------|---------|
| `Pages/Login.cshtml` | Login form UI with demo credentials display |
| `Pages/Login.cshtml.cs` | Login logic, cookie authentication, role-based redirect |
| `Pages/Logout.cshtml` | Logout redirect page |
| `Pages/Logout.cshtml.cs` | Logout handler, sign-out logic |

### Razor Pages - User Interface
| File | Purpose |
|------|---------|
| `Pages/Index.cshtml` | Welcome/home page |
| `Pages/Error.cshtml` | Error page for exceptions |
| `Pages/Error.cshtml.cs` | Error handling logic |
| `Pages/NotFound.cshtml` | 404 Not Found page |

### Razor Pages - Admin
| File | Purpose |
|------|---------|
| `Pages/Admin/Dashboard.cshtml` | Admin dashboard UI with transaction list and summary cards |
| `Pages/Admin/Dashboard.cshtml.cs` | Dashboard logic, filtering, calculations (Total In/Out/Charge) |

### Razor Pages - Staff
| File | Purpose |
|------|---------|
| `Pages/Staff/NewTransaction.cshtml` | Transaction entry form UI |
| `Pages/Staff/NewTransaction.cshtml.cs` | Form processing, transaction creation, validation |

### Documentation
| File | Purpose |
|------|---------|
| `README.md` | Project overview, features, getting started guide |
| `SETUP.md` | Installation and running instructions |
| `PROJECT_SUMMARY.md` | This file - detailed file listing and architecture |

### Static Files
| Directory | Purpose |
|-----------|---------|
| `wwwroot/css/` | CSS stylesheets (currently using Bootstrap CDN) |
| `wwwroot/js/` | JavaScript files (currently using Bootstrap CDN) |

## Architecture Decisions

### Authentication & Authorization

**Approach**: Cookie-based authentication with hardcoded credentials

**Implementation**:
- Two hardcoded users: admin/admin123 (Admin) and staff/staff123 (Staff)
- Claims-based identity with role claim
- AuthConstants class for centralized credential management

**Why This Approach**:
- Minimal dependencies (no ASP.NET Core Identity)
- Low overhead for small shops
- Easy to demonstrate and understand
- Can be easily replaced with proper OAuth (Gmail) later

### Database Design

**SQLite** selected for:
- No server required
- Single file database
- Low resource usage
- Perfect for small shops with low transaction volume
- Easy backup (copy the .db file)

**Single Table**: Transactions
- Stores all transaction records with metadata
- No complex relationships or normalization
- Simple, fast queries

### UI Framework

**Bootstrap 5.3 CDN**:
- No npm/build process needed
- Responsive design out of the box
- Minimal file size
- Perfect for low-spec systems

### Razor Pages Pattern

**Why Razor Pages over MVC**:
- Simpler for small applications
- Co-located page logic with UI
- Ideal for CRUD operations
- Easier to understand for beginners

## Key Features

### Dashboard (Admin Only)
```
Accessible at: /Admin/Dashboard
- Monthly transaction summary
- Total Amount In
- Total Amount Out
- Total Service Charge
- Filterable transaction list
- Filter by service type
- Filter by month
```

### Transaction Entry (Staff Only)
```
Accessible at: /Staff/NewTransaction
- Service type selection
- Amount in input
- Amount out input
- Service charge input
- Automatic timestamp
- Automatic user tracking
- Success feedback
```

### Login System
```
Accessible at: /Login
- Simple username/password form
- Hardcoded user database
- Role-based redirect after login
- 7-day persistent cookie
- Demo credentials displayed on login page
```

## Security Considerations

**Current Setup (For Demo)**:
- Hardcoded credentials in AuthConstants.cs
- No password hashing
- Cookies with 7-day expiration
- HTTPS recommended for production

**Actions for Production**:
1. Replace hardcoded credentials with database
2. Implement password hashing (bcrypt/PBKDF2)
3. Add SSL certificate
4. Implement Google/Gmail OAuth
5. Add rate limiting
6. Implement audit logging

## Performance Optimizations

1. **Lightweight Dependencies**: Only EF Core and Bootstrap
2. **Native AOT Compatible**: Code structure supports AOT compilation
3. **No Build Pipeline**: Bootstrap CDN eliminates npm/webpack
4. **Efficient Queries**: Direct LINQ to Entities
5. **SQLite**: Lightweight, no network overhead
6. **Async/Await**: All database operations are async
7. **Bootstrap CDN**: No local CSS compilation

## Deployment Ready

The application can be deployed to:
- Azure App Service
- Traditional IIS
- Docker containers
- Linux servers with .NET 8.0 runtime

### Deployment Commands

```bash
# Build for deployment
dotnet publish -c Release

# Publish to folder
dotnet publish -c Release -o ./publish

# Publish as self-contained
dotnet publish -c Release --self-contained -r win-x64
```

## Future Enhancement Path

### Phase 2: User Management
- [ ] Store users in database
- [ ] Password hashing
- [ ] User management interface
- [ ] Audit logging

### Phase 3: Authentication
- [ ] Google OAuth integration
- [ ] Gmail-based login
- [ ] MFA support
- [ ] Password reset functionality

### Phase 4: Advanced Features
- [ ] Advanced reporting
- [ ] PDF invoice generation
- [ ] Email notifications
- [ ] Backup/restore functionality
- [ ] Multi-language support

### Phase 5: Mobile
- [ ] Mobile companion app
- [ ] Offline transaction entry
- [ ] Sync when online

## Testing

### Manual Testing Scenarios

1. **Admin Login**
   - Login with admin/admin123
   - Verify dashboard loads
   - Test filters

2. **Staff Login**
   - Login with staff/staff123
   - Verify transaction form loads
   - Create test transaction
   - Verify in admin dashboard

3. **Authentication**
   - Test invalid credentials
   - Test logout
   - Test cookie expiration

## Troubleshooting Guide

### Application Won't Start
- Verify .NET 8.0 SDK is installed
- Check if ports 5000/5001 are available
- Delete shop.db if corrupted

### Database Errors
- Delete shop.db, shop.db-shm, shop.db-wal
- Restart application (creates new database)

### Authentication Issues
- Clear browser cookies
- Restart application
- Check AuthConstants.cs for credential config

### Performance Issues
- Monitor database file size
- Check transaction count
- Consider archiving old transactions

## Code Quality Notes

### Best Practices Implemented
✅ Async/await throughout  
✅ Dependency injection for DbContext  
✅ Authorization attributes on pages  
✅ Input validation on forms  
✅ Secure password (hardcoded demo only)  
✅ Separation of concerns (pages + code-behind)  
✅ Configuration-driven settings  
✅ Error handling with proper pages  
✅ HTML5 semantic markup  
✅ Responsive Bootstrap grid  

## Version Information

- **ASP.NET Core**: 8.0
- **C#**: 12
- **Entity Framework Core**: 8.0
- **SQLite**: 3
- **Bootstrap**: 5.3
- **.NET SDK Required**: 8.0+

## Maintenance

### Regular Tasks
- Monitor database file size
- Archive old transactions (monthly)
- Review access logs
- Update .NET SDK when new versions available

### Backup Strategy
```bash
# Simple backup (copy database file)
copy shop.db backups/shop.db.backup

# Automated scheduled backup recommended
```

## Support & Resources

- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Docs](https://learn.microsoft.com/en-us/ef/core/)
- [Bootstrap Documentation](https://getbootstrap.com/docs/)
- [Razor Pages Tutorial](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/)
- [SQLite Documentation](https://www.sqlite.org/docs.html)

---

**Created**: March 15, 2026  
**Version**: 1.0  
**Status**: Ready for Setup and First Run
