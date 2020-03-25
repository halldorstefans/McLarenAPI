using McLaren.Core.Entities;
using McLaren.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace McLaren.Infrastructure.Data
{
    public class McLarenContext : DbContext
    {
        public McLarenContext(DbContextOptions<McLarenContext> options) : base(options)
        {
        }

        public DbSet<GrandPrix> GrandPrix { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GrandPrixConfiguration());
            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new DriverConfiguration());
        }
    }
}