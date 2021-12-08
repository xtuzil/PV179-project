﻿using AutoMapper;
using BL.DTOs;
using BL.Services;
using Infrastructure.UnitOfWork;
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
        private IUnitOfWorkProvider unitOfWorkProvider;

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
            this.unitOfWorkProvider = unitOfWorkProvider; 
        }

        public async Task<List<ReviewDto>> GetTransferReviews(int transferId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return (List<ReviewDto>)await reviewService.GetReviewsOfTransfer(transferId);
            }
        }

        public async Task<bool> ApproveDelivery(int transferId, int userId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                var authorApproving = false;
                var recipientApproving = false;

                var transfer = await transferService.GetTransfer(transferId);
                
                if (transfer.Offer.AuthorId == userId)
                {
                    authorApproving = true;
                }

                if (transfer.Offer.RecipientId == userId)
                {
                    recipientApproving = true;
                }

                if (authorApproving || recipientApproving)
                {
                    await transferService.ApproveDelivery(transferId, authorApproving);

                    if ((authorApproving && transfer.RecipientAprovedDelivery) ||
                        (recipientApproving && transfer.AuthorAprovedDelivery))
                    {
                        await ProcessTransfer(transferId);
                    }

                    uow.Commit();
                    return true;
                }

                return false;
            }
        }

        public async Task<bool> ProcessTransfer(int transferId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                var transfer = await transferService.GetTransfer(transferId);

                if (!transfer.RecipientAprovedDelivery || !transfer.AuthorAprovedDelivery)
                {
                    return false;
                }

                var offer = await offerService.GetOffer(transfer.Offer.Id);

                // add offered money to each user
                await userService.AddUserMoneyAsync(offer.Author.Id, offer.RequestedMoney != null ? (double)offer.RequestedMoney : 0 );
                await userService.AddUserMoneyAsync(offer.Recipient.Id, offer.OfferedMoney != null ? (double)offer.OfferedMoney : 0);

                // add requested cactuses to author
                foreach (var cactusRequest in offer.RequestedCactuses)
                {
                    await cactusService.UpdateCactusOwnerAsync(cactusRequest.Cactus.Id, offer.Author.Id);
                }

                // add offered cactuses to recipient
                foreach (var cactusOffer in offer.OfferedCactuses)
                {
                    await cactusService.UpdateCactusOwnerAsync(cactusOffer.Cactus.Id, offer.Recipient.Id);
                }

                // set processed transfer time
                await transferService.SetTransferTimeAsync(transfer.Id);

                uow.Commit();
                return true;
            }
        }
    }
}
