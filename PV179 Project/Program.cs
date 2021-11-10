using AutoMapper;
using BL.Config;
using BL.DTOs;
using BL.Facades;
using BL.Services;
using BL;
using CactusDAL;
using CactusDAL.Models;
using Infrastructure.EntityFramework;
using Infrastructure.Predicates;
using Infrastructure.Predicates.Operators;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace PV179_Project
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var container = AutofacBLConfig.Configure();

            //this will be called from presentation layer
            var facade = container.Resolve<IUserCollectionFacade>();

            var allGenuses = facade.GetAllGenuses();
            Console.WriteLine($"All genuses:");
            foreach (var genusS in allGenuses)
            {
                Console.WriteLine($" {genusS.Name} with id {genusS.Id}");
            }

            var userFacade = container.Resolve<IUserFacade>();

            var usersWithName = await userFacade.GetAllUserWithNameAsync("Grace");
            foreach (var user in usersWithName)
            {
                Console.WriteLine($"Mr. {user.LastName} has firstname Grace and has these cactuses:");
                var cactuses = await facade.GetAllUserCactuses(user);
                foreach (var cactus in cactuses)
                {
                    Console.WriteLine($"  - Cactus with ID: {cactus.Id},  species ID: {cactus.SpeciesId}");
                }
                    

            }

            


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
