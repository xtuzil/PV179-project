using Autofac.Extras.Moq;
using BL.DTOs;
using BL.Facades;
using BL.Services;
using CactusDAL;
using Infrastructure.EntityFramework;
using Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BLTests
{
    public class UserFacadeTests
    {

        [Fact]
        public async Task GetUserTransfersTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var offer1 = new OfferDto { Id = 1 };
                var offer2 = new OfferDto { Id = 2 };
                var offer3 = new OfferDto { Id = 3 };

                var offers = new List<OfferDto> { offer1, offer2, offer3 };

                var user = new UserInfoDto { Id = 1 };

                var transfer1 = new TransferDto { Id = 1 };
                var transfer2 = new TransferDto { Id = 2 };
                var transfer3 = new TransferDto { Id = 3 };

                //Mock setup
                mock.Mock<IUnitOfWorkProvider>()
                   .Setup(x => x.Create())
                   .Returns(new EntityFrameworkUnitOfWork(() => new CactusDbContext()));

                mock.Mock<IOfferService>()
                   .Setup(x => x.GetTransferedOffersOfUser(user.Id))
                   .Returns(Task.Run(() => (IEnumerable<OfferDto>)offers));

                mock.Mock<ITransferService>()
                   .Setup(x => x.GetTransferByOfferId(offer1.Id))
                   .Returns(Task.Run(() => transfer1));

                mock.Mock<ITransferService>()
                   .Setup(x => x.GetTransferByOfferId(offer2.Id))
                   .Returns(Task.Run(() => transfer2));

                mock.Mock<ITransferService>()
                   .Setup(x => x.GetTransferByOfferId(offer3.Id))
                   .Returns(Task.Run(() => transfer3));


                var cls = mock.Create<UserFacade>();

                //Act
                var transfers = await cls.GetUserTransfers(user.Id);

                //Assert
                Assert.Contains(transfer1, transfers);
                Assert.Contains(transfer2, transfers);
                Assert.Contains(transfer3, transfers);

            }
        }
    }
}
