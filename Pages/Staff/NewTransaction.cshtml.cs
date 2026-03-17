using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagementSystem.Data;
using ShopManagementSystem.Models;

namespace ShopManagementSystem.Pages.Staff;

[Authorize]
public class NewTransactionModel : PageModel
{
    private readonly ShopDbContext _context;
    private readonly ILogger<NewTransactionModel> _logger;

    public NewTransactionModel(ShopDbContext context, ILogger<NewTransactionModel> logger)
    {
        _context = context;
        _logger = logger;
        RecentTransactions = new List<Transaction>();
    }

    public List<Transaction> RecentTransactions { get; set; }

    public void OnGet()
    {
        _logger.LogInformation("=== NewTransaction Page OnGet - User: {User} IsStaff: {IsStaff} ===", 
            User?.Identity?.Name ?? "Anonymous", AuthConstants.IsStaff(User));
        
        // Verify user is Staff
        if (!AuthConstants.IsStaff(User))
        {
            _logger.LogError("Unauthorized access to NewTransaction - User: {User} is not Staff", User?.Identity?.Name);
            throw new UnauthorizedAccessException("Only staff can access this page.");
        }

        // Load recent transactions for this user
        var userName = User.Identity?.Name ?? "Unknown";
        RecentTransactions = _context.Transactions
            .Where(t => t.CreatedBy == userName)
            .OrderByDescending(t => t.Timestamp)
            .Take(10)
            .ToList();

        _logger.LogInformation("NewTransaction access granted for staff: {User} - Found {Count} recent transactions", 
            userName, RecentTransactions.Count);
    }

    public async Task<IActionResult> OnPostAsync(string serviceType, decimal amountIn, 
        decimal amountOut, decimal serviceCharge)
    {
        _logger.LogInformation("=== NewTransaction OnPost - User: {User} ServiceType: {ServiceType} ===", 
            User?.Identity?.Name ?? "Anonymous", serviceType);
        
        // Verify user is Staff
        if (!AuthConstants.IsStaff(User))
        {
            _logger.LogError("Unauthorized POST to NewTransaction - User: {User}", User?.Identity?.Name);
            return new ForbidResult();
        }

        if (string.IsNullOrEmpty(serviceType) || amountIn < 0 || amountOut < 0 || serviceCharge < 0)
        {
            ModelState.AddModelError(string.Empty, "Please fill in all required fields with valid values.");
            _logger.LogWarning("Invalid transaction data from user: {User}", User?.Identity?.Name);
            
            // Reload recent transactions on validation failure
            var userName = User.Identity?.Name ?? "Unknown";
            RecentTransactions = _context.Transactions
                .Where(t => t.CreatedBy == userName)
                .OrderByDescending(t => t.Timestamp)
                .Take(10)
                .ToList();
            
            return Page();
        }

        var transaction = new Transaction
        {
            Timestamp = DateTime.Now,
            ServiceType = serviceType,
            AmountIn = amountIn,
            AmountOut = amountOut,
            ServiceCharge = serviceCharge,
            CreatedBy = User.Identity?.Name ?? "Unknown"
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Transaction saved successfully - User: {User} ServiceType: {ServiceType} Amount: {Amount}", 
            User.Identity?.Name, serviceType, amountIn);

        TempData["SuccessMessage"] = $"✓ Transaction saved! {serviceType} - Rs. {amountIn:N2}";
        return RedirectToPage();
    }
}
