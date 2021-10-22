﻿using CactusDAL;
using CactusDAL.Models;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PV179_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new CactusDbContext("Server=(localdb)\\mssqllocaldb;Integrated Security=True;MultipleActiveResultSets=True;Database=CactusesManager;Trusted_Connection=True;"))
            {
                db.Database.EnsureDeleted();    // DEV: just so that we don't have to drop the DB manually
                db.Database.EnsureCreated();

                db.Cactuses.Add(new CactusDAL.Models.Cactus { Species = db.Species.First(), Owner = db.Users.First(), ForSale = false });
                db.SaveChanges();

                var offer1 = db.Offers.Include(o => o.Comments).Where(o => o.Id == 1).First();
                System.Console.WriteLine("Comment:", offer1.Comments.First().Text);

                var offer = db.Offers
                    .Include(o => o.PreviousOffer)
                    .Include(o => o.CactusOffers)
                    .Include(o => o.CactusRequests)
                    .Where(o => o.Id == 2)
                    .First();

                //offer = db.Offers.Where(o => o.Id == 2).First();
                //offer = db.Offers.UseFilter();


                //using (var uowp = new EntityFrameworkUnitOfWorkProvider(() => new CactusDbContext()))
                //{
                //    uowp.Create();
                //    IUnitOfWork uow = uowp.GetUnitOfWorkInstance();
                //    uow.Users.Create(new User { FirstName = "Jack", LastName = "Smith", Email = "example@example.com", Password = "password", AddressId = 1, AccountBalance = 50 });
                //    uow.Commit();
                //}

                var uowp = new EntityFrameworkUnitOfWorkProvider(() => new CactusDbContext());
                using (var uow = uowp.Create())
                {
                    var users = new EntityFrameworkRepository<User>(uowp);
                    users.Create(new User { FirstName = "Jack", LastName = "Smith", Email = "example@example.com", Password = "password", AddressId = 1, AccountBalance = 50 });
                    uow.Commit();
                }


                //System.Console.WriteLine(offer.PreviousOffer.Response);
                //System.Console.WriteLine(offer.CactusOffers.First().Amount);
                //System.Console.WriteLine(offer.Comments.ToList().Count);
                //System.Console.WriteLine(offer.RequestedCactuses.First().Transfers.First().Cactus.CreationDate); 

                //System.Console.WriteLine(db.Users.Include(u => u.TransfersTo).Where(u => u.Id == 3).First().TransfersTo.First());
            }
        }
    }
}
