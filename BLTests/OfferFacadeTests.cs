using Autofac.Extras.Moq;
using BL.DTOs;
using BL.Exceptions;
using BL.Facades;
using BL.Services;
using BL.Services.Interfaces;
using CactusDAL;
using CactusDAL.Models;
using Infrastructure.EntityFramework;
using Infrastructure.UnitOfWork;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BLTests
{
    public class OfferFacadeTests
    {


        [Fact]
        public async Task CreateOfferTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange

                var author = new UserInfoDto
                {
                    Id = 1,
                    AccountBalance = 50,
                };

                var recipient = new UserInfoDto
                {
                    Id = 2,
                    AccountBalance = 0,
                };

                var cactus1 = new CactusDto
                {
                    Id = 1,
                    Owner = author,
                    Amount = 100
                };

                var cactus2 = new CactusDto
                {
                    Id = 2,
                    Owner = recipient,
                    Amount = 50
                };

                var cactusOffers = new Dictionary<int, int>{
                    { cactus1.Id, 30 }
                };

                var cactusRequests = new Dictionary<int, int>{
                    { cactus2.Id, 20 }
                };

                var offer = new OfferCreateDto
                {
                    OfferedMoney = 45,
                    RequestedMoney = 0,
                    OfferedCactuses = cactusOffers,
                    RequestedCactuses = cactusRequests,
                };

                var createdOffer = new Offer
                {
                    Id = 5,
                    OfferedMoney = 45,
                    RequestedMoney = 0,
                };

                //Mock setup
                mock.Mock<IUnitOfWorkProvider>()
                   .Setup(x => x.Create())
                   .Returns(new EntityFrameworkUnitOfWork(() => new CactusDbContext()));

                mock.Mock<IOfferService>()
                   .Setup(x => x.CreateOffer(offer))
                   .ReturnsAsync(createdOffer);

                mock.Mock<ICactusOfferService>()
                   .Setup(x => x.AddCactusOffer(createdOffer.Id, cactusOffers.Keys.ElementAt(0), cactusOffers.Values.ElementAt(0)))
                   .Returns(Task.Run(() => { }));


                mock.Mock<ICactusOfferService>()
                   .Setup(x => x.AddCactusRequest(createdOffer.Id, cactusRequests.Keys.ElementAt(0), cactusRequests.Values.ElementAt(0)))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ICactusService>()
                    .Setup(x => x.GetCactus(cactus1.Id))
                    .ReturnsAsync(cactus1);

                var cls = mock.Create<OfferFacade>();

                //Act
                var obtainedCreatedOffer = await cls.CreateOffer(offer);

                //Assert
                Assert.Equal(createdOffer.Id, obtainedCreatedOffer.Id);
            }
        }

        [Fact]
        public async Task AcceptOfferTest()
        {
            using (var mock = AutoMock.GetLoose())
            {

                //Arrange

                var author = new UserInfoDto
                {
                    Id = 1,
                    AccountBalance = 50,
                };

                var recipient = new UserInfoDto
                {
                    Id = 2,
                    AccountBalance = 0,
                };

                var cactus1 = new CactusDto
                {
                    Id = 1,
                    Owner = author,
                    Amount = 100
                };

                var cactus2 = new CactusDto
                {
                    Id = 2,
                    Owner = recipient,
                    Amount = 50
                };

                var cactus3 = new Cactus
                {
                    Id = 3,
                    Amount = 70
                };

                var cactus4 = new Cactus
                {
                    Id = 4,
                    Amount = 30
                };


                var cactusOffers = new List<CactusOfferDto>{
                    new CactusOfferDto{
                        Amount = 30,
                        Cactus = cactus1
                    }
                };

                var cactusRequests = new List<CactusOfferDto>{
                    new CactusOfferDto{
                        Amount = 20,
                        Cactus = cactus2
                    }
                };

                var offer = new OfferDto
                {
                    Id = 5,
                    Author = author,
                    Recipient = recipient,
                    OfferedMoney = 45,
                    RequestedMoney = 0,
                    OfferedCactuses = cactusOffers,
                    RequestedCactuses = cactusRequests,
                };

                var acceptedOffer = new OfferDto
                {
                    Id = 5,
                    Author = author,
                    Recipient = recipient,
                    OfferedMoney = 45,
                    RequestedMoney = 0,
                    OfferedCactuses = cactusOffers,
                    RequestedCactuses = cactusRequests,
                    Response = BL.Enums.OfferStatus.Accepted
                };

                //Mock setup
                mock.Mock<IUnitOfWorkProvider>()
                   .Setup(x => x.Create())
                   .Returns(new EntityFrameworkUnitOfWork(() => new CactusDbContext()));

                mock.Mock<IOfferService>()
                   .Setup(x => x.AcceptOffer(offer.Id))
                   .Returns(Task.Run(() => acceptedOffer));

                mock.Mock<IUserService>()
                   .Setup(x => x.RemoveUserMoneyAsync(author.Id, (double)offer.OfferedMoney))
                   .Returns(Task.Run(() => { }));

                mock.Mock<IUserService>()
                   .Setup(x => x.RemoveUserMoneyAsync(recipient.Id, (double)offer.RequestedMoney))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ICactusService>()
                   .Setup(x => x.RemoveCactusFromUser(cactus1.Id));

                mock.Mock<ICactusService>()
                   .Setup(x => x.RemoveCactusFromUser(cactus2.Id));

                mock.Mock<ICactusService>()
                   .Setup(x => x.CreateNewCactusInstanceForTransfer(cactusOffers[0].Cactus, cactusOffers[0].Amount))
                   .ReturnsAsync(cactus3);

                mock.Mock<ICactusService>()
                   .Setup(x => x.CreateNewCactusInstanceForTransfer(cactusRequests[0].Cactus, cactusRequests[0].Amount))
                   .ReturnsAsync(cactus4);

                mock.Mock<ICactusOfferService>()
                   .Setup(x => x.UpdateCactusOfferCactusAsync(cactusOffers[0].Id, cactus3.Id))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ICactusOfferService>()
                   .Setup(x => x.UpdateCactusOfferCactusAsync(cactusRequests[0].Id, cactus4.Id))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ITransferService>()
                   .Setup(x => x.CreateTransfer(offer.Id))
                   .Returns(Task.Run(() => { }));


                var cls = mock.Create<OfferFacade>();

                //Act
                try
                {
                    await cls.AcceptOfferAsync(offer.Id);
                }
                catch (InsufficientMoneyException)
                {
                    Assert.True(false, "No exception should have been thrown");
                }
            }
        }


        [Fact]
        public async Task AcceptOfferLowMoneyTest()
        {
            using (var mock = AutoMock.GetLoose())
            {

                //Arrange

                var author = new UserInfoDto
                {
                    Id = 1,
                    AccountBalance = 50,
                };

                var recipient = new UserInfoDto
                {
                    Id = 2,
                    AccountBalance = 0,
                };

                var cactus1 = new CactusDto
                {
                    Id = 1,
                    Owner = author,
                    Amount = 100
                };

                var cactus2 = new CactusDto
                {
                    Id = 2,
                    Owner = recipient,
                    Amount = 50
                };

                var cactus3 = new Cactus
                {
                    Id = 3,
                    Amount = 70
                };

                var cactus4 = new Cactus
                {
                    Id = 4,
                    Amount = 30
                };


                var cactusOffers = new List<CactusOfferDto>{
                    new CactusOfferDto{
                        Amount = 30,
                        Cactus = cactus1
                    }
                };

                var cactusRequests = new List<CactusOfferDto>{
                    new CactusOfferDto{
                        Amount = 20,
                        Cactus = cactus2
                    }
                };

                var offer = new OfferDto
                {
                    Id = 5,
                    Author = author,
                    Recipient = recipient,
                    OfferedMoney = 105,
                    RequestedMoney = 0,
                    OfferedCactuses = cactusOffers,
                    RequestedCactuses = cactusRequests,
                };

                var acceptedOffer = new OfferDto
                {
                    Id = 5,
                    Author = author,
                    Recipient = recipient,
                    OfferedMoney = 45,
                    RequestedMoney = 0,
                    OfferedCactuses = cactusOffers,
                    RequestedCactuses = cactusRequests,
                    Response = BL.Enums.OfferStatus.Accepted
                };

                //Mock setup
                mock.Mock<IUnitOfWorkProvider>()
                   .Setup(x => x.Create())
                   .Returns(new EntityFrameworkUnitOfWork(() => new CactusDbContext()));

                mock.Mock<IOfferService>()
                   .Setup(x => x.AcceptOffer(offer.Id))
                   .Throws(new InsufficientMoneyException());

                mock.Mock<IUserService>()
                   .Setup(x => x.RemoveUserMoneyAsync(author.Id, (double)offer.OfferedMoney))
                   .Returns(Task.Run(() => { }));

                mock.Mock<IUserService>()
                   .Setup(x => x.RemoveUserMoneyAsync(recipient.Id, (double)offer.RequestedMoney))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ICactusService>()
                   .Setup(x => x.RemoveCactusFromUser(cactus1.Id));

                mock.Mock<ICactusService>()
                   .Setup(x => x.RemoveCactusFromUser(cactus2.Id));

                mock.Mock<ICactusService>()
                   .Setup(x => x.CreateNewCactusInstanceForTransfer(cactusOffers[0].Cactus, cactusOffers[0].Amount))
                   .ReturnsAsync(cactus3);

                mock.Mock<ICactusService>()
                   .Setup(x => x.CreateNewCactusInstanceForTransfer(cactusRequests[0].Cactus, cactusRequests[0].Amount))
                   .ReturnsAsync(cactus4);

                mock.Mock<ICactusOfferService>()
                   .Setup(x => x.UpdateCactusOfferCactusAsync(cactusOffers[0].Id, cactus3.Id))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ICactusOfferService>()
                   .Setup(x => x.UpdateCactusOfferCactusAsync(cactusRequests[0].Id, cactus4.Id))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ITransferService>()
                   .Setup(x => x.CreateTransfer(offer.Id))
                   .Returns(Task.Run(() => { }));


                var cls = mock.Create<OfferFacade>();

                //Act + Assert
                await Assert.ThrowsAsync<InsufficientMoneyException>(async () => await cls.AcceptOfferAsync(offer.Id));

            }
        }

        [Fact]
        public async Task RejectOfferTest()
        {
            using (var mock = AutoMock.GetLoose())
            {

                //Arrange

                var author = new UserInfoDto
                {
                    Id = 1,
                    AccountBalance = 50,
                };

                var recipient = new UserInfoDto
                {
                    Id = 2,
                    AccountBalance = 0,
                };

                var cactus1 = new CactusDto
                {
                    Id = 1,
                    Owner = author,
                    Amount = 100
                };


                var cactusOffers = new List<CactusOfferDto>{
                    new CactusOfferDto{
                        Amount = 30,
                        Cactus = cactus1
                    }
                };



                var offer = new OfferDto
                {
                    Id = 5,
                    Author = author,
                    Recipient = recipient,
                    OfferedMoney = 45,
                    RequestedMoney = 0,
                    OfferedCactuses = cactusOffers,
                    Response = BL.Enums.OfferStatus.Created
                };

                var rejectedOffer = new OfferDto
                {
                    Id = 5,
                    Author = author,
                    AuthorId = author.Id,
                    Recipient = recipient,
                    OfferedMoney = 45,
                    RequestedMoney = 0,
                    OfferedCactuses = cactusOffers,
                    Response = BL.Enums.OfferStatus.Declined
                };


                //Mock setup
                mock.Mock<IUnitOfWorkProvider>()
                   .Setup(x => x.Create())
                   .Returns(new EntityFrameworkUnitOfWork(() => new CactusDbContext()));

                mock.Mock<IOfferService>()
                   .Setup(x => x.GetOffer(offer.Id))
                   .Returns(Task.Run(() => offer));

                mock.Mock<IOfferService>()
                   .Setup(x => x.UpdateOfferStatus(offer.Id, OfferStatus.Declined))
                   .Returns(Task.Run(() => rejectedOffer));

                mock.Mock<IUserService>()
                   .Setup(x => x.AddUserMoneyAsync(author.Id, (double)offer.OfferedMoney))
                   .Returns(Task.Run(() => { }));


                mock.Mock<ICactusService>()
                   .Setup(x => x.UpdateCactusOwnerAsync(cactus1.Id, author.Id));


                var cls = mock.Create<OfferFacade>();

                //Act

                await cls.RejectOffer(offer.Id);

            }

        }

        [Fact]
        public async Task RemoveOfferTest()
        {
            using (var mock = AutoMock.GetLoose())
            {

                //Arrange

                var author = new UserInfoDto
                {
                    Id = 1,
                    AccountBalance = 50,
                };

                var recipient = new UserInfoDto
                {
                    Id = 2,
                    AccountBalance = 0,
                };

                var cactus1 = new CactusDto
                {
                    Id = 1,
                    Owner = author,
                    Amount = 100
                };


                var cactusOffers = new List<CactusOfferDto>{
                    new CactusOfferDto{
                        Id = 1,
                        Amount = 30,
                        Cactus = cactus1
                    }
                };

                var cactusRequests = new List<CactusOfferDto> { };

                var offer = new OfferDto
                {
                    Id = 5,
                    Author = author,
                    Recipient = recipient,
                    OfferedMoney = 45,
                    RequestedMoney = 0,
                    OfferedCactuses = cactusOffers,
                    RequestedCactuses = cactusRequests,
                    Response = BL.Enums.OfferStatus.Created
                };

                var rejectedOffer = new OfferDto
                {
                    Id = 5,
                    Author = author,
                    AuthorId = author.Id,
                    Recipient = recipient,
                    OfferedMoney = 45,
                    RequestedMoney = 0,
                    OfferedCactuses = cactusOffers,
                    Response = BL.Enums.OfferStatus.Declined
                };


                //Mock setup
                mock.Mock<IUnitOfWorkProvider>()
                   .Setup(x => x.Create())
                   .Returns(new EntityFrameworkUnitOfWork(() => new CactusDbContext()));

                mock.Mock<IOfferService>()
                   .Setup(x => x.GetOffer(offer.Id))
                   .Returns(Task.Run(() => offer));

                mock.Mock<IOfferService>()
                   .Setup(x => x.RemoveOffer(offer.Id))
                   .Returns(Task.Run(() => rejectedOffer));

                mock.Mock<IUserService>()
                   .Setup(x => x.AddUserMoneyAsync(author.Id, (double)offer.OfferedMoney))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ICactusOfferService>()
                   .Setup(x => x.RemoveCactusOffer(cactusOffers[0].Id))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ICactusService>()
                   .Setup(x => x.UpdateCactusOwnerAsync(cactus1.Id, author.Id));


                var cls = mock.Create<OfferFacade>();

                //Act

                await cls.RemoveOffer(offer.Id);

            }

        }
    }

}

