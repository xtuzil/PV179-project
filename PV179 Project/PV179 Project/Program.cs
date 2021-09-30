using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PV179_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new CactusesManagerDbContext("Server=(localdb)\\mssqllocaldb;Integrated Security=True;MultipleActiveResultSets=True;Database=CactusesManager;Trusted_Connection=True;"))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Cactuses.Add(new Data_Access_Layer.Models.Cactus { Species = db.Species.First(), Owner = db.Users.First(), ForSale = false });
                db.SaveChanges();

                var offer = db.Offers
                    .Include(o => o.PreviousOffer)
                    .Include(o => o.OfferedCactuses)
                    .Include(o => o.RequestedCactuses)
                    .Include(o => o.Shipment)
                    .Where(o => o.Id == 2)
                    .First();

                System.Console.WriteLine(offer.PreviousOffer.Response);
                System.Console.WriteLine(offer.OfferedCactuses.ToList().Count);
                System.Console.WriteLine(offer.RequestedCactuses.ToList().Count);
                System.Console.WriteLine(offer.Shipment.Status);
            }
        }
    }
}
