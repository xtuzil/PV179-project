using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer
{
    public class CactusesManagerDbContext : DbContext
    {
        public DbSet<Cactus> Cactuses { get; set; }
        public DbSet<TradeOffer> CounterOffers { get; set; }
        public DbSet<Genus> Genuses { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        private string connectionString = "Server=(localdb)\\mssqllocaldb;Integrated Security=True;MultipleActiveResultSets=True;Database=CactusesManager;Trusted_Connection=True;";

        public CactusesManagerDbContext()
        {

        }

        public CactusesManagerDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
