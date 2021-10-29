using AutoMapper;
using BL.Config;
using BL.DTOs;
using BL.Facades;
using CactusDAL;
using CactusDAL.Models;
using Infrastructure.EntityFramework;
using Infrastructure.Predicates;
using Infrastructure.Predicates.Operators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PV179_Project
{
    class Program
    {
        static async Task Main(string[] args)
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

                using (var uow = uowp.Create())
                {
                    var usersQuery = new EntityFrameworkQuery<User>(uowp);
                    IPredicate predicate = new SimplePredicate(nameof(User.FirstName), "Jack", ValueComparingOperator.Equal);
                    usersQuery.Where(predicate);
                    var result = await usersQuery.ExecuteAsync();

                    Console.WriteLine("Result: " + result.Items.First().LastName);
                }

                Genus genus;
                using (var uow = uowp.Create())
                {
                    var genuses = new EntityFrameworkRepository<Genus>(uowp);
                    genus = await genuses.GetAsync(1);
                }
                var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
                GenusDto genusdto =  mapper.Map<GenusDto>(genus);

                var myFacade = new UserCollectionFacade();

                var usersWithName = await myFacade.GetAllUserWithNameAsync("Aston");

                foreach (var user in usersWithName)
                {
                    Console.WriteLine($"Mr. {user.LastName} has firstname Aston");
                }

                var approvedSpecies = await myFacade.GetAllApprovedSpeciesWithGenus(genusdto);

                Console.WriteLine($"Approved species for genus {genus.Name} count: {approvedSpecies.Count}");

                foreach (var species in approvedSpecies)
                {
                    Console.WriteLine($" {species.Name} is approved species for genus {genus.Name}");
                }

                var allGenuses = myFacade.GetAllGenuses();
                Console.WriteLine($"All genuses:");
                foreach (var genusS in allGenuses)
                {
                    Console.WriteLine($" {genusS.Name} with id {genusS.Id}");
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
