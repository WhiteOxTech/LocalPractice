using System.Security.Claims;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using ShopManagementSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// Configure Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

var logger = builder.Services.AddLogging().BuildServiceProvider().GetRequiredService<ILogger<Program>>();
logger.LogInformation("=== Application Starting ===");
logger.LogInformation("Environment: {Environment}", builder.Environment.EnvironmentName);

// Add DbContext with SQLite
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Data Source=shop.db";
builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlite(connectionString));

// Add Authentication and Authorization
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = new PathString("/Login");
        options.AccessDeniedPath = new PathString("/AccessDenied");
        // Cookie path will be set dynamically based on the request path base
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("role", "Admin"));
    options.AddPolicy("StaffOnly", policy => policy.RequireClaim("role", "Staff"));
});

// Configure Data Protection for persistent key storage (required for IIS)
var contentRoot = builder.Environment.ContentRootPath;
var keysFolder = Path.Combine(contentRoot, "keys");
if (!Directory.Exists(keysFolder))
{
    Directory.CreateDirectory(keysFolder);
}

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(keysFolder));

builder.Services.AddRazorPages();

var app = builder.Build();

// Create logger
var appLogger = app.Services.GetRequiredService<ILogger<Program>>();
appLogger.LogInformation("=== Middleware Configuration Starting ===");

// Add request logging middleware
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation(">>> Incoming Request: {Method} {Path}{QueryString} User={User}", 
        context.Request.Method, context.Request.Path, context.Request.QueryString, context.User?.Identity?.Name ?? "Anonymous");
    
    await next();
    
    logger.LogInformation("<<< Response: StatusCode={StatusCode} Path={Path} User={User}", 
        context.Response.StatusCode, context.Request.Path, context.User?.Identity?.Name ?? "Anonymous");
    
    // Check for redirects
    if (context.Response.StatusCode >= 300 && context.Response.StatusCode < 400)
    {
        var location = context.Response.Headers["Location"].ToString();
        logger.LogWarning("!!! REDIRECT: {StatusCode} -> {Location}", 
            context.Response.StatusCode, location);
    }
});

appLogger.LogInformation("=== Middleware Configuration Complete ===");

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
    dbContext.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseStaticFiles();

// Add status code pages for 404 and other HTTP errors
app.UseStatusCodePagesWithReExecute("/NotFound");

app.UseRouting();

app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation(">>> Authentication Check: Path={Path} IsAuthenticated={IsAuthenticated} User={User}",
        context.Request.Path, context.User?.Identity?.IsAuthenticated ?? false, context.User?.Identity?.Name ?? "Anonymous");
    
    await next();
    
    logger.LogInformation("<<< After Authentication: IsAuthenticated={IsAuthenticated} User={User}",
        context.User?.Identity?.IsAuthenticated ?? false, context.User?.Identity?.Name ?? "Anonymous");
});

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
