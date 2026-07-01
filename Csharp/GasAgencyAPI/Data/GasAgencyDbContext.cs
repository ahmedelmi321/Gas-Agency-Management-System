using GasAgencyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GasAgencyAPI.Data
{
    public class GasAgencyDbContext : DbContext
    {
        public GasAgencyDbContext(DbContextOptions<GasAgencyDbContext> options)
            : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<GasCylinder> GasCylinders { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GasCylinder>().ToTable("GasCylinder");

            // The SQL schema defines these primary keys as plain INT PRIMARY KEY
            // (no IDENTITY), so callers must supply the ID themselves on Create.
            // Without this, EF Core's default convention would try to let SQL
            // Server auto-generate them, which fails since the columns aren't
            // configured as IDENTITY in the database.
            modelBuilder.Entity<Customer>().Property(c => c.CustomerID).ValueGeneratedNever();
            modelBuilder.Entity<GasCylinder>().Property(g => g.CylinderID).ValueGeneratedNever();
            modelBuilder.Entity<Sale>().Property(s => s.SaleID).ValueGeneratedNever();

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.GasCylinder)
                .WithMany(g => g.Sales)
                .HasForeignKey(s => s.CylinderID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
