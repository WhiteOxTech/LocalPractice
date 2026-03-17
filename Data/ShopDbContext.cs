using Microsoft.EntityFrameworkCore;
using ShopManagementSystem.Models;

namespace ShopManagementSystem.Data;

public class ShopDbContext : DbContext
{
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Transaction table configuration
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Timestamp).IsRequired();
            entity.Property(e => e.ServiceType).HasMaxLength(100).IsRequired();
            entity.Property(e => e.AmountIn).HasPrecision(10, 2);
            entity.Property(e => e.AmountOut).HasPrecision(10, 2);
            entity.Property(e => e.ServiceCharge).HasPrecision(10, 2);
            entity.Property(e => e.CreatedBy).HasMaxLength(100).IsRequired();
        });
    }
}
