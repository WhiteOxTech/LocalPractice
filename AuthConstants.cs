namespace ShopManagementSystem;

/// <summary>
/// Utility class for authentication-related operations.
/// Provides hardcoded credentials for demonstration.
/// </summary>
public static class AuthConstants
{
    // Hardcoded credentials for demo (replace with actual authentication in production)
    public static readonly Dictionary<string, (string Password, string Role)> Users = new()
    {
        { "admin", ("admin123", "Admin") },
        { "staff", ("staff123", "Staff") }
    };

    public const string AdminRole = "Admin";
    public const string StaffRole = "Staff";

    /// <summary>
    /// Gets the user role from claims.
    /// </summary>
    public static string? GetUserRole(System.Security.Claims.ClaimsPrincipal user)
    {
        return user.FindFirst("role")?.Value;
    }

    /// <summary>
    /// Checks if the user is an admin.
    /// </summary>
    public static bool IsAdmin(System.Security.Claims.ClaimsPrincipal user)
    {
        return GetUserRole(user)?.Equals(AdminRole, StringComparison.OrdinalIgnoreCase) ?? false;
    }

    /// <summary>
    /// Checks if the user is staff.
    /// </summary>
    public static bool IsStaff(System.Security.Claims.ClaimsPrincipal user)
    {
        return GetUserRole(user)?.Equals(StaffRole, StringComparison.OrdinalIgnoreCase) ?? false;
    }
}
