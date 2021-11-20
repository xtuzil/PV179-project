using AutoMapper;
using BL.DTOs;
using BL.Services;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class TransferFacade : ITransferFacade
    {
        private IMapper mapper;
        private ITransferService transferService;
        private IReviewService reviewService;
        private IUserService userService;
        private ICactusService cactusService;
        private IOfferService offerService;
        private IUnitOfWorkProvider uowp;

        public TransferFacade(IMapper mapper,
            ITransferService transferService,
            IReviewService reviewService,
            ICactusService cactusService,
            IOfferService offerService,
            IUserService userService,
        IUnitOfWorkProvider unitOfWorkProvider
        )
        {
            this.mapper = mapper;
            this.transferService = transferService;
            this.reviewService = reviewService;
            this.cactusService = cactusService;
            this.offerService = offerService;
            this.userService = userService;
            uowp = unitOfWorkProvider;
        }

        public async Task<List<ReviewDto>> GetTransferReviews(int transferId)
        {
            using (var uow = uowp.Create())
            {
                return (List<ReviewDto>)await reviewService.GetReviewsOfTransfer(transferId);
            }
        }

        public async void ProcessTransfer(int transferId)
        {
            var transfer = await transferService.GetTransfer(transferId);

            // add offered money from each user
            await userService.AddUserMoneyAsync(transfer.Offer.Author.Id, (double)transfer.Offer.RequestedMoney);
            await userService.AddUserMoneyAsync(transfer.Offer.Recipient.Id, (double)transfer.Offer.OfferedMoney);


            // add offer cactuses to each user
            foreach (var cactusOffer in transfer.Offer.OfferedCactuses)
            {
                cactusOffer.Cactus.Owner = transfer.Offer.Recipient;
                cactusService.UpdateCactusInformation(cactusOffer.Cactus);
            }

            foreach (var cactusRequest in transfer.Offer.RequestedCactuses)
            {
                cactusRequest.Cactus.Owner = transfer.Offer.Author;
                cactusService.UpdateCactusInformation(cactusRequest.Cactus);
            }

            // set process transfer time
            transfer.TransferedTime = DateTime.UtcNow;
            transferService.UpdateTransfer(transfer);


        }
    }
}
