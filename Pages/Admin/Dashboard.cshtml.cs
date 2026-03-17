using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagementSystem.Data;
using ShopManagementSystem.Models;

namespace ShopManagementSystem.Pages.Admin;

[Authorize]
public class DashboardModel : PageModel
{
    private readonly ShopDbContext _context;
    private readonly ILogger<DashboardModel> _logger;

    public List<Transaction> Transactions { get; set; } = new();
    public decimal TotalAmountIn { get; set; }
    public decimal TotalAmountOut { get; set; }
    public decimal TotalServiceCharge { get; set; }
    public string? SelectedServiceType { get; set; }
    public string? SelectedMonth { get; set; }

    public DashboardModel(ShopDbContext context, ILogger<DashboardModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void OnGet(string? serviceType, string? month)
    {
        _logger.LogInformation("=== Dashboard Page OnGet - User: {User} IsAdmin: {IsAdmin} ===", 
            User?.Identity?.Name ?? "Anonymous", AuthConstants.IsAdmin(User));
        
        // Verify user is Admin
        if (!AuthConstants.IsAdmin(User))
        {
            _logger.LogError("Unauthorized access to Dashboard - User: {User} is not Admin", User?.Identity?.Name);
            throw new UnauthorizedAccessException("Only admins can access this page.");
        }

        _logger.LogInformation("Dashboard access granted for admin: {User}", User?.Identity?.Name);

        SelectedServiceType = serviceType;
        SelectedMonth = string.IsNullOrEmpty(month) ? DateTime.Now.ToString("yyyy-MM") : month;

        // Parse month
        if (DateTime.TryParseExact(SelectedMonth, "yyyy-MM", null, System.Globalization.DateTimeStyles.None, out var selectedDate))
        {
            var monthStart = new DateTime(selectedDate.Year, selectedDate.Month, 1);
            var monthEnd = monthStart.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

            // Query transactions for the month
            var query = _context.Transactions
                .Where(t => t.Timestamp >= monthStart && t.Timestamp <= monthEnd);

            // Filter by service type if provided
            if (!string.IsNullOrEmpty(serviceType))
            {
                query = query.Where(t => t.ServiceType == serviceType);
            }

            Transactions = query.OrderByDescending(t => t.Timestamp).ToList();

            // Calculate totals
            TotalAmountIn = Transactions.Sum(t => t.AmountIn);
            TotalAmountOut = Transactions.Sum(t => t.AmountOut);
            TotalServiceCharge = Transactions.Sum(t => t.ServiceCharge);
        }
    }
}
