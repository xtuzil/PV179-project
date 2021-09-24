using Data_Access_Layer;
using System;

namespace PV179_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new CactusesManagerDbContext("Server=(localdb)\\mssqllocaldb;Integrated Security=True;MultipleActiveResultSets=True;Database=CactusesManager;Trusted_Connection=True;"))
            {
                db.Database.EnsureCreated();
                db.Cactuses.Add(new Data_Access_Layer.Models.Cactus() { });
                db.SaveChanges();
            }
        }
    }
}
