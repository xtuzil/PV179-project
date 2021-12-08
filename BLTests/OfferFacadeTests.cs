using Autofac.Extras.Moq;
using BL.DTOs;
using BL.Facades;
using BL.Services;
using BL.Services.Interfaces;
using CactusDAL;
using CactusDAL.Models;
using Infrastructure.EntityFramework;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BLTests
{
    public class OfferFacadeTests
    {


        [Fact]
        public void CreateOfferTest()
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

                var cactusOffers = new List<CactusOfferCreateDto>{
                    new CactusOfferCreateDto{
                        Amount = 30,
                        CactusId = cactus1.Id,
                    }
                };

                var cactusRequests = new List<CactusOfferCreateDto>{
                    new CactusOfferCreateDto{
                        Amount = 20,
                        CactusId = cactus2.Id,
                    }
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
                   .Returns(createdOffer);

                mock.Mock<ICactusOfferService>()
                   .Setup(x => x.AddCactusOffer(cactusOffers[0]));


                mock.Mock<ICactusOfferService>()
                   .Setup(x => x.AddCactusRequest(cactusRequests[0]));

                var cls = mock.Create<OfferFacade>();

                //Act
                var obtainedCreatedOffer = cls.CreateOffer(offer);

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
                        CactusId = cactus1.Id,
                        Cactus = cactus1
                    }
                };

                var cactusRequests = new List<CactusOfferDto>{
                    new CactusOfferDto{
                        Amount = 20,
                        CactusId = cactus2.Id,
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
                   .Setup(x => x.RemoveCactusFromUser(cactus1));

                mock.Mock<ICactusService>()
                   .Setup(x => x.RemoveCactusFromUser(cactus2));

                mock.Mock<ICactusService>()
                   .Setup(x => x.CreateNewCactusInstanceForTransfer(cactusOffers[0].Cactus, cactusOffers[0].Amount))
                   .Returns(cactus3);

                mock.Mock<ICactusService>()
                   .Setup(x => x.CreateNewCactusInstanceForTransfer(cactusRequests[0].Cactus, cactusRequests[0].Amount))
                   .Returns(cactus4);

                mock.Mock<ICactusOfferService>()
                   .Setup(x => x.UpdateCactusOfferCactusAsync(cactusOffers[0].Id, cactus3.Id))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ICactusOfferService>()
                   .Setup(x => x.UpdateCactusOfferCactusAsync(cactusRequests[0].Id, cactus4.Id))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ITransferService>()
                   .Setup(x => x.CreateTransfer(offer.Id));


                var cls = mock.Create<OfferFacade>();

                //Act
                var status = await cls.AcceptOfferAsync(offer);

                //Assert
                Assert.True(status);
 
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
                        CactusId = cactus1.Id,
                        Cactus = cactus1
                    }
                };

                var cactusRequests = new List<CactusOfferDto>{
                    new CactusOfferDto{
                        Amount = 20,
                        CactusId = cactus2.Id,
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
                   .Returns(Task.Run(() => acceptedOffer));

                mock.Mock<IUserService>()
                   .Setup(x => x.RemoveUserMoneyAsync(author.Id, (double)offer.OfferedMoney))
                   .Returns(Task.Run(() => { }));

                mock.Mock<IUserService>()
                   .Setup(x => x.RemoveUserMoneyAsync(recipient.Id, (double)offer.RequestedMoney))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ICactusService>()
                   .Setup(x => x.RemoveCactusFromUser(cactus1));

                mock.Mock<ICactusService>()
                   .Setup(x => x.RemoveCactusFromUser(cactus2));

                mock.Mock<ICactusService>()
                   .Setup(x => x.CreateNewCactusInstanceForTransfer(cactusOffers[0].Cactus, cactusOffers[0].Amount))
                   .Returns(cactus3);

                mock.Mock<ICactusService>()
                   .Setup(x => x.CreateNewCactusInstanceForTransfer(cactusRequests[0].Cactus, cactusRequests[0].Amount))
                   .Returns(cactus4);

                mock.Mock<ICactusOfferService>()
                   .Setup(x => x.UpdateCactusOfferCactusAsync(cactusOffers[0].Id, cactus3.Id))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ICactusOfferService>()
                   .Setup(x => x.UpdateCactusOfferCactusAsync(cactusRequests[0].Id, cactus4.Id))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ITransferService>()
                   .Setup(x => x.CreateTransfer(offer.Id));


                var cls = mock.Create<OfferFacade>();

                //Act
                var status = await cls.AcceptOfferAsync(offer);

                //Assert
                Assert.False(status);

            }
        }
    }
}