using Autofac;
using BL;
using BL.DTOs;
using BL.Facades;
using CactusDAL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PV179_Project
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // DEV: just so that we don't have to drop the DB manually
            using (var db = new CactusDbContext("Server=(localdb)\\mssqllocaldb;Integrated Security=True;MultipleActiveResultSets=True;Database=CactusesManager;Trusted_Connection=True;"))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

            }

            var container = AutofacBLConfig.Configure();

            var cactusFacade = container.Resolve<ICactusFacade>();

            await cactusFacade.ProposeNewSpecies(new SpeciesCreateDto { GenusId = 1, Name = "ProposedName", LatinName = "Veni vidi vici" });

            //this will be called from presentation layer
            var facade = container.Resolve<IUserCollectionFacade>();

            var allGenuses = await facade.GetAllGenuses();
            Console.WriteLine($"All genuses:");
            foreach (var genusS in allGenuses)
            {
                Console.WriteLine($" {genusS.Name} with id {genusS.Id}");
            }

            var userFacade = container.Resolve<IUserFacade>();

            userFacade.CreateUser(new UserCreateDto { FirstName = "Jackie", LastName = "Smiths", Email = "example@example.com", Password = "password" });

            var usersWithNameAston = await userFacade.GetAllUserWithNameAsync("Aston");
            foreach (var user in usersWithNameAston)
            {
                Console.WriteLine($"Mr. {user.LastName} has firstname Aston and has these cactuses:");
                var cactuses = await facade.GetAllUserCactuses(user.Id);
                foreach (var cactus in cactuses.Items)
                {
                    Console.WriteLine($"  - Cactus with ID: {cactus.Id},  species ID: {cactus.Species.Name}");
                }


            }

            var offerFacade = container.Resolve<IOfferFacade>();

            var cactus1 = await cactusFacade.GetCactus(1);
            var cactus2 = await cactusFacade.GetCactus(2);


            var cactusOffers = new Dictionary<int, int>{
                { cactus1.Id, 30 }
            };

            var cactusRequests = new Dictionary<int, int>{
                { cactus2.Id, 20 }
            };

            var offer = new OfferCreateDto
            {
                AuthorId = 2,
                RecipientId = 4,
                OfferedMoney = 45,
                RequestedMoney = 0,
                OfferedCactuses = cactusOffers,
                RequestedCactuses = cactusRequests,
            };

            var createdOffer = await offerFacade.CreateOffer(offer);


            var getOffer = await offerFacade.GetOffer(createdOffer.Id);

            foreach (var co in getOffer.OfferedCactuses)
            {
                Console.WriteLine($" CactusOffer with ID: {co.Id}");
            }

            await offerFacade.AcceptOfferAsync(getOffer.Id);

            var transferFacade = container.Resolve<ITransferFacade>();

            await transferFacade.ProcessTransfer(2);


            //Console.WriteLine($"Offer with iD: {createdOffer.Id} with offered money: {createdOffer.OfferedMoney} and author Id: {createdOffer.Author.Id}");







            /*Genus genus;
            var genuses = new EntityFrameworkRepository<Genus>(uowp);

            using (var uow = uowp.Create())
            {
                genus = await genuses.GetAsync(1);
            }
            var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
            GenusDto genusdto = mapper.Map<GenusDto>(genus);

          

            var approvedSpecies = await facade.GetAllApprovedSpeciesWithGenus(genusdto);

            Console.WriteLine($"Approved species for genus {genus.Name} count: {approvedSpecies.Count}");

            foreach (var species in approvedSpecies)
            {
                Console.WriteLine($" {species.Name} is approved species for genus {genus.Name}");
            }*/



            /*
            var services = new ServiceCollection();
            services.AddScoped<IUnitOfWorkProvider>(serviceProvier => new EntityFrameworkUnitOfWorkProvider(() => new CactusDbContext()));
            services.AddSingleton(serviceProvider => new UserService(serviceProvider.GetRequiredService<IUnitOfWorkProvider>()));
            services.AddSingleton(serviceProvider => new SpeciesService(serviceProvider.GetRequiredService<IUnitOfWorkProvider>()));
            services.AddSingleton(serviceProvider => new GenusService(serviceProvider.GetRequiredService<IUnitOfWorkProvider>()));
            services.AddSingleton(serviceProvider => new UserFacade(serviceProvider.GetRequiredService<IUnitOfWorkProvider>(), serviceProvider.GetRequiredService<UserService>()));
            services.AddSingleton(serviceProvider => new UserCollectionFacade(
                serviceProvider.GetRequiredService<IUnitOfWorkProvider>(),
                serviceProvider.GetRequiredService<UserService>(),
                serviceProvider.GetRequiredService<SpeciesService>(),
                serviceProvider.GetRequiredService<GenusService>())
            );

            var provider = services.BuildServiceProvider();

            var uowp = provider.GetService<IUnitOfWorkProvider>();
            var users = new EntityFrameworkRepository<User>(uowp);

            using (var uow = uowp.Create())
            {
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
            var genuses = new EntityFrameworkRepository<Genus>(uowp);

            using (var uow = uowp.Create())
            {
                genus = await genuses.GetAsync(1);
            }
            var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
            GenusDto genusdto =  mapper.Map<GenusDto>(genus);

            var myFacade = provider.GetService<UserCollectionFacade>();

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
            } */

            //System.Console.WriteLine(offer.PreviousOffer.Response);
            //System.Console.WriteLine(offer.CactusOffers.First().Amount);
            //System.Console.WriteLine(offer.Comments.ToList().Count);
            //System.Console.WriteLine(offer.RequestedCactuses.First().Transfers.First().Cactus.CreationDate); 

            //System.Console.WriteLine(db.Users.Include(u => u.TransfersTo).Where(u => u.Id == 3).First().TransfersTo.First());

        }
    }
}
