using CactusDAL;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PV179_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new CactusDbContext("Server=(localdb)\\mssqllocaldb;Integrated Security=True;MultipleActiveResultSets=True;Database=Cactus;Trusted_Connection=True;"))
            {
                db.Database.EnsureDeleted();    // DEV: just so that we don't have to drop the DB manually
                db.Database.EnsureCreated();

                db.Cactuses.Add(new CactusDAL.Models.Cactus { Species = db.Species.First(), Owner = db.Users.First(), ForSale = false });
                db.SaveChanges();

                var offer = db.Offers
                    .Include(o => o.PreviousOffer)
                    .Include(o => o.OfferedCactuses)
                    .Include(o => o.RequestedCactuses)
                    .ThenInclude(c => c.Transfers)
                    .Where(o => o.Id == 2)
                    .First();

                System.Console.WriteLine(offer.PreviousOffer.Response);
                System.Console.WriteLine(offer.OfferedCactuses.ToList().Count);
                System.Console.WriteLine(offer.RequestedCactuses.ToList().Count);
                System.Console.WriteLine(offer.RequestedCactuses.First().Species.Name);

                System.Console.WriteLine(db.Users.Include(u => u.TransfersTo).Where(u => u.Id == 3).First().TransfersTo.First());
            }
        }
    }
}
