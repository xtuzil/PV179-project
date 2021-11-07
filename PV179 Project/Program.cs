using BL.Facades;
using BL.Services;
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

namespace PV179_Project
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddScoped<IUnitOfWorkProvider>(serviceProvier => new EntityFrameworkUnitOfWorkProvider(() => new CactusDbContext()));
            services.AddSingleton(serviceProvider => new UserService(serviceProvider.GetRequiredService<IUnitOfWorkProvider>()));
            services.AddSingleton(serviceProvider => new UserFacade(serviceProvider.GetRequiredService<IUnitOfWorkProvider>(), serviceProvider.GetRequiredService<UserService>()));

            var provider = services.BuildServiceProvider();

            var uowp = provider.GetService<IUnitOfWorkProvider>();
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

            var userFacade = provider.GetService<UserFacade>();
            var userService = provider.GetService<UserService>();

            var usersWithNameAston = await userFacade.GetAllUserWithNameAsync("Aston");

            foreach (var user in usersWithNameAston)
            {
                Console.WriteLine($"Mr. {user.LastName} has firstname Aston");
            }

            //System.Console.WriteLine(offer.PreviousOffer.Response);
            //System.Console.WriteLine(offer.CactusOffers.First().Amount);
            //System.Console.WriteLine(offer.Comments.ToList().Count);
            //System.Console.WriteLine(offer.RequestedCactuses.First().Transfers.First().Cactus.CreationDate); 

            //System.Console.WriteLine(db.Users.Include(u => u.TransfersTo).Where(u => u.Id == 3).First().TransfersTo.First());
            
        }
    }
}
