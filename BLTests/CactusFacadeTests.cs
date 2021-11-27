using Autofac.Extras.Moq;
using BL.DTOs;
using BL.Facades;
using BL.Services;
using CactusDAL;
using Infrastructure.EntityFramework;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BLTests
{
    public class CactusFacadeTests
    {

        [Fact]
        public async Task CreateOfferTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var genus = new GenusDto
                {
                    Id = 1,
                    Name = "Echinocactus"
                };

                var species = new SpeciesDto
                {
                    Id = 1,
                    Name = "Golden Barrel",
                    LatinName = "Echinocactus grusonii",
                    Genus = genus,
                    Approved = true
                };

                var species2 = new SpeciesDto
                {
                    Id = 2,
                    Name = "Silver Ball",
                    LatinName = "Parodia scopa",
                    Genus = genus,
                    Approved = true
                };


                var cactus1 = new CactusDto
                {
                    Id = 1,
                    Amount = 100,
                    Species = species
                };

                var cactus2 = new CactusDto
                {
                    Id = 2,
                    Amount = 50,
                    Species = species
                };

                var cactus3 = new CactusDto
                {
                    Id = 3,
                    Amount = 50,
                    Species = species2
                };

                var specieses = new List<SpeciesDto> { species, species2 };
                var cactuses = new List<CactusDto> { cactus1, cactus2, cactus3 };

                //Mock setup
                mock.Mock<IUnitOfWorkProvider>()
                   .Setup(x => x.Create())
                   .Returns(new EntityFrameworkUnitOfWork(() => new CactusDbContext()));

                mock.Mock<ISpeciesService>()
                   .Setup(x => x.getAllApprovedSpeciesWithGenus(genus.Id))
                   .Returns(Task.Run(() => (IEnumerable<SpeciesDto>)specieses));

                mock.Mock<ICactusService>()
                   .Setup(x => x.GetCactusesWithSpecies(specieses))
                   .Returns(Task.Run(() => (IEnumerable<CactusDto>) cactuses));


                var cls = mock.Create<CactusFacade>();

                //Act
                var obtainedCactuses = await cls.GetCactusesWithGenus(genus.Id);

                //Assert
                Assert.Equal(cactuses.Count, obtainedCactuses.Count);
            }
        }
    }
}
