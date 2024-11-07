using HealthGuard.Core.Entities;
using HealthGuard.Core.Entities.Disease;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthGuard.DataAccess.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Image> Images { get; set; }

        public DbSet<Symptom> Symptoms { get; set; }

        public DbSet<Disease> Diseases { get; set; }

        public DbSet<Treatment> Treatments { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<TransmissionMethod> TransmissionMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Ignore(e => e.PhoneNumberConfirmed)
                .Ignore(e => e.TwoFactorEnabled);

            modelBuilder.Entity<Disease>()
                .HasMany(d => d.Symptoms)
                .WithMany(s => s.Diseases);

            modelBuilder.Entity<Disease>()
                .HasMany(d => d.Treatments)
                .WithMany(t => t.Diseases);

            modelBuilder.Entity<Disease>()
                .HasMany(d => d.TransmissionMethods)
                .WithMany(t => t.Diseases);
        }
    }
}
