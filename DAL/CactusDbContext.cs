using CactusDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CactusDAL
{
    public class CactusDbContext : DbContext
    {
        public DbSet<Cactus> Cactuses { get; set; }
        public DbSet<Genus> Genuses { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<User> Users { get; set; }

        private string connectionString = "Server=(localdb)\\mssqllocaldb;Integrated Security=True;MultipleActiveResultSets=True;Database=CactusesManager;Trusted_Connection=True;";

        public CactusDbContext()
        {

        }

        public CactusDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Offer>()
                .HasOne(o => o.PreviousOffer)
                .WithOne(o => o.NextOffer);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Sender)
                .WithMany(u => u.OffersSent);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Receiver)
                .WithMany(u => u.OffersReceived);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Author)
                .WithMany(u => u.ReportsSent);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Target)
                .WithMany(u => u.ReportsReceived);

            modelBuilder.Entity<Species>()
                .HasOne(s => s.SuggestedBy)
                .WithMany(u => u.SpeciesSuggested);

            modelBuilder.Entity<Species>()
                .HasOne(s => s.ConfirmedBy)
                .WithMany(u => u.SpeciesConfirmed);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Author)
                .WithMany(u => u.ReviewsSent);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.ReviewsReceived);

            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.From)
                .WithMany(u => u.TransfersFrom);

            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.To)
                .WithMany(u => u.TransfersTo);

            modelBuilder.Entity<Cactus>()
                .HasMany(c => c.OfferedIn)
                .WithMany(o => o.OfferedCactuses)
                .UsingEntity<CactusOffered>(
                    j => j
                        .HasOne(co => co.Offer)
                        .WithMany(o => o.CactusOffers)
                        .HasForeignKey(co => co.OfferId),
                    j => j
                        .HasOne(co => co.Cactus)
                        .WithMany(c => c.CactusOffers)
                        .HasForeignKey(co => co.CactusId),
                    j =>
                    {
                        j.HasKey(co => new { co.CactusId, co.OfferId });
                    });

            modelBuilder.Entity<Cactus>()
                .HasMany(c => c.RequestedIn)
                .WithMany(o => o.RequestedCactuses)
                .UsingEntity<CactusRequested>(
                    j => j
                        .HasOne(co => co.Offer)
                        .WithMany(o => o.CactusRequests)
                        .HasForeignKey(co => co.OfferId),
                    j => j
                        .HasOne(co => co.Cactus)
                        .WithMany(c => c.CactusRequests)
                        .HasForeignKey(co => co.CactusId),
                    j =>
                    {
                        j.HasKey(co => new { co.CactusId, co.OfferId });
                    });

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            // automatically set CreationDate to current time
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is DatedEntity && e.State == EntityState.Added);

            foreach (var entityEntry in entries)
            {
                ((DatedEntity)entityEntry.Entity).CreationDate = DateTime.Now;
            }

            return base.SaveChanges();
        }
    }
}
