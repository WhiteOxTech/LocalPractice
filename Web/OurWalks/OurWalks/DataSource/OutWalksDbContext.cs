using Microsoft.EntityFrameworkCore;
using OurWalks.Model;

namespace OurWalks.DataSource
{
    public class OutWalksDbContext : DbContext
    {
        public OutWalksDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulty { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Region>().HasData(
                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "BENG",
                    Name = "Bengalore"
                }
            //    ,
            //new Difficulty
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Low"
            //}
            );
        }
    }
}