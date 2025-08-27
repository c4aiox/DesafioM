using Microsoft.EntityFrameworkCore;
using BikeRental.Api.Models;

namespace BikeRental.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Motorcycle>().ToTable("motos");
            modelBuilder.Entity<Motorcycle>().Property(m => m.Id ).HasColumnName("identificador");
            modelBuilder.Entity<Motorcycle>().Property(m => m.Year).HasColumnName("ano");
            modelBuilder.Entity<Motorcycle>().Property(m => m.Model).HasColumnName("modelo");
            modelBuilder.Entity<Motorcycle>().Property(m => m.Plate).HasColumnName("placa");
        }
    }
}
