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
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<PostalAddress> PostalAddresses { get; set; }

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
                .HasOne(o => o.PreviousOffer);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Sender)
                .WithMany(u => u.OffersSent);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Receiver)
                .WithMany(u => u.OffersReceived);

            modelBuilder.Entity<Offer>()
                .HasMany(o => o.CactusOffers)
                .WithOne(cactusOffer => cactusOffer.Offer);


            modelBuilder.Entity<Species>()
                .HasMany(s => s.WishlistedBy)
                .WithMany(u => u.Wishlist);

            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.SenderReview)
                .WithOne()
                .HasForeignKey<Transfer>(t => t.SenderReviewId);

            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.ReceiverReview)
                .WithOne()
                .HasForeignKey<Transfer>(t => t.ReceiverReviewId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Transfer)
                .WithOne(t => t.SenderReview);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Author);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Target);

            modelBuilder.Entity<Species>()
                .HasOne(s => s.SuggestedBy);

            modelBuilder.Entity<Species>()
                .HasOne(s => s.ConfirmedBy);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Author);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.ReviewsReceived);


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
