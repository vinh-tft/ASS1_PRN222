using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace DataAccess
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        // Seed data (nếu muốn có sẵn vài xe trong DB)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, Name = "Tesla Model S", Configuration = "Electric, AWD", Price = 2500, IsElectric = true, CreatedAt = DateTime.Now },
                new Car { Id = 2, Name = "Toyota Camry", Configuration = "2.5L, FWD", Price = 1000, IsElectric = false, CreatedAt = DateTime.Now },
                new Car { Id = 3, Name = "BMW i8", Configuration = "Hybrid, AWD", Price = 3500, IsElectric = true, CreatedAt = DateTime.Now }
            );
        }
    }
}
