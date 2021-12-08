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
    public class TransferFacadeTests
    {
        [Fact]
        public async Task ProcessTransferTest()
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
                    Response = BL.Enums.OfferStatus.Accepted
                };

                var transfer = new TransferDto
                {
                    Id = 1,
                    Offer = offer,
                    AuthorAprovedDelivery = true,
                    RecipientAprovedDelivery = true
                };




                //Mock setup
                mock.Mock<IUnitOfWorkProvider>()
                   .Setup(x => x.Create())
                   .Returns(new EntityFrameworkUnitOfWork(() => new CactusDbContext()));

                mock.Mock<ITransferService>()
                   .Setup(x => x.GetTransfer(transfer.Id))
                   .Returns(Task.Run(() => transfer));

                mock.Mock<IOfferService>()
                   .Setup(x => x.GetOffer(transfer.Offer.Id))
                   .Returns(Task.Run(() => offer));

                mock.Mock<IUserService>()
                   .Setup(x => x.AddUserMoneyAsync(author.Id, (double)offer.RequestedMoney))
                   .Returns(Task.Run(() => { }));

                mock.Mock<IUserService>()
                   .Setup(x => x.RemoveUserMoneyAsync(recipient.Id, (double)offer.OfferedMoney))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ICactusService>()
                   .Setup(x => x.UpdateCactusOwnerAsync(offer.RequestedCactuses[0].Id, author.Id))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ICactusService>()
                   .Setup(x => x.UpdateCactusOwnerAsync(offer.OfferedCactuses[0].Id, recipient.Id))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ITransferService>()
                   .Setup(x => x.SetTransferTimeAsync(transfer.Id))
                   .Returns(Task.Run(() => { }));


                var cls = mock.Create<TransferFacade>();

                //Act
                var status = await cls.ProcessTransfer(transfer.Id);

                //Assert
                Assert.True(status);
            }
        }


        [Fact]
        public async Task ProcessTransferNotApprovedYetTest()
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
                    Response = BL.Enums.OfferStatus.Accepted
                };

                var transfer = new TransferDto
                {
                    Id = 1,
                    Offer = offer,
                    AuthorAprovedDelivery = false,
                    RecipientAprovedDelivery = true
                };




                //Mock setup
                mock.Mock<IUnitOfWorkProvider>()
                   .Setup(x => x.Create())
                   .Returns(new EntityFrameworkUnitOfWork(() => new CactusDbContext()));

                mock.Mock<ITransferService>()
                   .Setup(x => x.GetTransfer(transfer.Id))
                   .Returns(Task.Run(() => transfer));

                mock.Mock<IOfferService>()
                   .Setup(x => x.GetOffer(transfer.Offer.Id))
                   .Returns(Task.Run(() => offer));

                mock.Mock<IUserService>()
                   .Setup(x => x.AddUserMoneyAsync(author.Id, (double)offer.RequestedMoney))
                   .Returns(Task.Run(() => { }));

                mock.Mock<IUserService>()
                   .Setup(x => x.RemoveUserMoneyAsync(recipient.Id, (double)offer.OfferedMoney))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ICactusService>()
                   .Setup(x => x.UpdateCactusOwnerAsync(offer.RequestedCactuses[0].Id, author.Id))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ICactusService>()
                   .Setup(x => x.UpdateCactusOwnerAsync(offer.OfferedCactuses[0].Id, recipient.Id))
                   .Returns(Task.Run(() => { }));

                mock.Mock<ITransferService>()
                   .Setup(x => x.SetTransferTimeAsync(transfer.Id))
                   .Returns(Task.Run(() => { }));


                var cls = mock.Create<TransferFacade>();

                //Act
                var status = await cls.ProcessTransfer(transfer.Id);

                //Assert
                Assert.False(status);
            }
        }


        [Fact]
        public async Task CreateReviewTest()
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
                    AuthorId = author.Id,
                    Recipient = recipient,
                    RecipientId = recipient.Id,
                    OfferedMoney = 45,
                    RequestedMoney = 0,
                    OfferedCactuses = cactusOffers,
                    RequestedCactuses = cactusRequests,
                    Response = BL.Enums.OfferStatus.Accepted
                };


                var transfer = new TransferDto
                {
                    Id = 1,
                    Offer = offer,
                    AuthorAprovedDelivery = false,
                    RecipientAprovedDelivery = true
                };

                var review = new ReviewCreateDto
                {
                    Text = "Everything went smooth!",
                    Score = 4.50,
                    AuthorId = author.Id,
                    TransferId = 1
                };



                //Mock setup
                mock.Mock<IUnitOfWorkProvider>()
                   .Setup(x => x.Create())
                   .Returns(new EntityFrameworkUnitOfWork(() => new CactusDbContext()));

                mock.Mock<ITransferService>()
                   .Setup(x => x.GetTransfer(transfer.Id))
                   .Returns(Task.Run(() => transfer));


                mock.Mock<IReviewService>()
                   .Setup(x => x.CreateReview(review))
                   .Returns(Task.Run(() => { }));



                var cls = mock.Create<TransferFacade>();

                //Act
                var status = await cls.CreateReview(review);

                //Assert
                Assert.True(status);
            }
        }

        [Fact]
        public async Task CreateReviewReviewAlreadyExistsTest()
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
                    AuthorId = author.Id,
                    Recipient = recipient,
                    RecipientId = recipient.Id,
                    OfferedMoney = 45,
                    RequestedMoney = 0,
                    OfferedCactuses = cactusOffers,
                    RequestedCactuses = cactusRequests,
                    Response = BL.Enums.OfferStatus.Accepted
                };

                var existingReview = new ReviewDto
                {
                    Text = "Everything went smooth!",
                    Score = 4.50,
                    Author = author,
                };

                var transfer = new TransferDto
                {
                    Id = 1,
                    Offer = offer,
                    AuthorAprovedDelivery = true,
                    RecipientAprovedDelivery = true,
                    AuthorReview = existingReview
                  
                };

               
                var review = new ReviewCreateDto
                {
                    Text = "Everything went smooth!",
                    Score = 4.50,
                    AuthorId = author.Id,
                    TransferId = 1
                };


                //Mock setup
                mock.Mock<IUnitOfWorkProvider>()
                   .Setup(x => x.Create())
                   .Returns(new EntityFrameworkUnitOfWork(() => new CactusDbContext()));

                mock.Mock<ITransferService>()
                   .Setup(x => x.GetTransfer(transfer.Id))
                   .Returns(Task.Run(() => transfer));


                mock.Mock<IReviewService>()
                   .Setup(x => x.CreateReview(review))
                   .Returns(Task.Run(() => { }));



                var cls = mock.Create<TransferFacade>();

                //Act
                var status = await cls.CreateReview(review);

                //Assert
                Assert.False(status);
            }
        }
    }
}
