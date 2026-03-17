using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShopManagementSystem.Pages;

public class LoginModel : PageModel
{
    private readonly ILogger<LoginModel> _logger;

    public string? ErrorMessage { get; set; }

    public LoginModel(ILogger<LoginModel> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> OnPostAsync(string? username, string? password)
    {
        _logger.LogInformation("Login attempt: username={Username}", username);
        
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ErrorMessage = "Username and password are required.";
            _logger.LogWarning("Login failed: Empty username or password");
            return Page();
        }

        if (!AuthConstants.Users.TryGetValue(username.ToLower(), out var userInfo))
        {
            ErrorMessage = "Invalid username or password.";
            _logger.LogWarning("Login failed: User not found - {Username}", username);
            return Page();
        }

        if (userInfo.Password != password)
        {
            ErrorMessage = "Invalid username or password.";
            _logger.LogWarning("Login failed: Invalid password for user - {Username}", username);
            return Page();
        }

        _logger.LogInformation("Login successful: username={Username} role={Role}", username, userInfo.Role);

        // Create claims for the user
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim("role", userInfo.Role)
        };

        var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
        };

        await HttpContext.SignInAsync(
            "CookieAuth",
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        _logger.LogInformation("SignInAsync completed for user: {Username}", username);

        // Redirect based on role
        var redirectPage = userInfo.Role == "Admin" 
            ? "/Admin/Dashboard" 
            : "/Staff/NewTransaction";
        
        _logger.LogInformation("Redirecting user {Username} to {Page}", username, redirectPage);
        
        return userInfo.Role == "Admin" 
            ? RedirectToPage("/Admin/Dashboard") 
            : RedirectToPage("/Staff/NewTransaction");
    }
}
