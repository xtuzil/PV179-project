using CactusDAL.Models;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CactusDAL
{
    public class CactusDbContext : DbContext
    {
       
        public DbSet<Genus> Genuses { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Cactus> Cactuses { get; set; }
        public DbSet<CactusPhoto> CactusPhotos { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<ProfilePhoto> ProfilePhotos { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<PostalAddress> PostalAddresses { get; set; }


        public DbSet<Offer> Offers { get; set; }
        public DbSet<CactusOffer> CactusOffers { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Review> Reviews { get; set; }
        

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
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Offer>()
                .HasOne(o => o.PreviousOffer);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Author)
                .WithMany(u => u.OffersSent);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Recipient)
                .WithMany(u => u.OffersReceived);

            modelBuilder.Entity<Offer>()
               .HasMany(o => o.CactusOffers)
               .WithOne(co => co.Offer)
               .HasForeignKey(co => co.OfferId);


            // Todo: not neccessary
            modelBuilder.Entity<Offer>()
                .HasMany(o => o.Comments)
                .WithOne(c => c.Offer)
                .HasForeignKey(c => c.OfferId);


           /* modelBuilder.Entity<Offer>()
              .HasMany(o => o.CactusRequests)
              .WithOne(co => co.Offer)
              .HasForeignKey(cr => cr.OfferId);*/

      



            /*modelBuilder.Entity<Offer>()
                .HasMany(o => o.CactusOffers)
                .WithOne(cactusOffer => cactusOffer.Offer);*/

            /*modelBuilder.Entity<Offer>()
                .HasMany(o => o.CactusRequests)
                .WithOne(cactusRequest => cactusRequest.Offer);*/

            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.AuthorReview)
                .WithOne()
                .HasForeignKey<Transfer>(t => t.AuthorReviewId);

            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.RecipientReview)
                .WithOne()
                .HasForeignKey<Transfer>(t => t.RecipientReviewId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Transfer)
                .WithOne(t => t.AuthorReview);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Author);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Target);

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
