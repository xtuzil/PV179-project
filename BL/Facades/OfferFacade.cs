using AutoMapper;
using BL.Config;
using BL.DTOs;
using BL.Services;
using BL.Services.Interfaces;
using CactusDAL.Models;
using Infrastructure.UnitOfWork;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class OfferFacade : IOfferFacade
    {
        private IUnitOfWorkProvider unitOfWorkProvider;
        private IOfferService _offerService;
        private IUserService _userService;
        private ICactusService _cactusService;
        private ICactusOfferService _cactusOfferService;
        private ITransferService _transferService;

        public OfferFacade(IUnitOfWorkProvider unitOfWorkProvider,
            IOfferService offerService,
            IUserService userService,
            ICactusService cactusService,
            ICactusOfferService cactusOfferService,
            ITransferService transferService)
        {
            this.unitOfWorkProvider = unitOfWorkProvider;
            _offerService = offerService;
            _userService = userService;
            _cactusService = cactusService;
            _cactusOfferService = cactusOfferService;
            _transferService = transferService;
        }

        public async Task<OfferDto> CreateOffer(OfferCreateDto offer)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                var createdOffer = await _offerService.CreateOffer(offer);

                if (offer.PreviousOfferId != null)
                {
                    // updating status of previous offer to Counteroffer status
                    await _offerService.UpdateOfferStatus(offer.PreviousOfferId.Value, OfferStatus.Counteroffer);
                }

                uow.Commit();

                // remove (block) offered money from author
                await _userService.RemoveUserMoneyAsync(offer.AuthorId, (double)offer.OfferedMoney);

                // remove (block) offered cactuses from author
                foreach (var cactusOffered in offer.OfferedCactuses)
                {
                    var cactus = await _cactusService.GetCactus(cactusOffered.Key);

                    // If author offered all pieces of the cactus instance we want to remove owner
                    // Otherwise, we will remove only amount
                    if (cactus.Amount == cactusOffered.Value)
                    {
                        await _cactusService.RemoveCactusFromUser(cactus.Id);
                    }
                    else
                    {
                        await _cactusService.UpdateCactusAmountAsync(cactus.Id, -cactusOffered.Value);
                    }
                    //creating cactusOffer
                    await _cactusOfferService.AddCactusOffer(createdOffer.Id, cactusOffered.Key, cactusOffered.Value);
                }

                foreach (var cactusRequested in offer.RequestedCactuses)
                {
                    // creating cactusRequest
                    await _cactusOfferService.AddCactusRequest(createdOffer.Id, cactusRequested.Key, cactusRequested.Value);
                }
                uow.Commit();

                // Mapping in facade is neccessary because of retrieving id
                var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
                return mapper.Map<OfferDto>(createdOffer);
            }
        }

        public async Task AcceptOfferAsync(int offerId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                var offer = await _offerService.AcceptOffer(offerId);

                // Remove money from recipient
                await _userService.RemoveUserMoneyAsync(offer.Recipient.Id, (double)offer.RequestedMoney);


                foreach (var cactusOffer in offer.OfferedCactuses)
                {
                    // If author offered only few pieces of his cactus, it is neccessary to create new instance for transfer 
                    if (cactusOffer.Cactus.Owner != null)
                    {
                        var newCatus = await _cactusService.CreateNewCactusInstanceForTransfer(cactusOffer.Cactus, cactusOffer.Amount);
                        uow.Commit();
                        await _cactusOfferService.UpdateCactusOfferCactusAsync(cactusOffer.Id, newCatus.Id);
                    }
                }

                foreach (var cactusRequest in offer.RequestedCactuses)
                {
                    if (cactusRequest.Cactus.Amount - cactusRequest.Amount <= 0)
                    {
                        await _cactusService.RemoveCactusFromUser(cactusRequest.Cactus.Id);
                    }
                    else
                    {
                        // remove amount of cactuses from user
                        await _cactusService.UpdateCactusAmountAsync(cactusRequest.Cactus.Id, -cactusRequest.Amount);

                        // create new instance of cactus for transfer
                        var newCatus = await _cactusService.CreateNewCactusInstanceForTransfer(cactusRequest.Cactus, cactusRequest.Amount);
                        uow.Commit();
                        await _cactusOfferService.UpdateCactusRequestCactusAsync(cactusRequest.Id, newCatus.Id);
                    }
                }

                // create Transfer object in db
                await _transferService.CreateTransfer(offer.Id);

                uow.Commit();
            }
        }


        public async Task<OfferDto> GetOffer(int offerId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return await _offerService.GetOffer(offerId);
            }
        }

        public async Task RejectOffer(int offerId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                await _offerService.UpdateOfferStatus(offerId, OfferStatus.Declined);

                // add resources back to author 
                var offer = await _offerService.GetOffer(offerId);

                await _userService.AddUserMoneyAsync(offer.AuthorId, (double)offer.OfferedMoney);

                // add offer cactuses back to author
                foreach (var cactusOffer in offer.OfferedCactuses)
                {
                    if (cactusOffer.Cactus.Owner != null)
                    {
                        await _cactusService.UpdateCactusAmountAsync(cactusOffer.Cactus.Id, cactusOffer.Amount);
                    }
                    else
                    {
                        await _cactusService.UpdateCactusOwnerAsync(cactusOffer.Cactus.Id, offer.AuthorId);
                    }
                }

                uow.Commit();
            }
        }

        public async Task<bool> RemoveOffer(int offerId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                var offer = await _offerService.GetOffer(offerId);

                // Offer can be deleted only when recipient did not answer yet
                if (offer.Response != Enums.OfferStatus.Created)
                {
                    return false;
                }

                // add resources back to author 
                await _userService.AddUserMoneyAsync(offer.AuthorId, (double)offer.OfferedMoney);

                // add offer cactuses back to author
                foreach (var cactusOffer in offer.OfferedCactuses)
                {
                    if (cactusOffer.Cactus.Owner != null)
                    {
                        await _cactusService.UpdateCactusAmountAsync(cactusOffer.Cactus.Id, cactusOffer.Amount);
                    }
                    else
                    {
                        await _cactusService.UpdateCactusOwnerAsync(cactusOffer.Cactus.Id, offer.AuthorId);
                    }
                }

                // remove CactusOffers
                foreach (var cactusOffer in offer.OfferedCactuses)
                {
                    await _cactusOfferService.RemoveCactusOffer(cactusOffer.Id);
                }

                // remove CactusRequests
                foreach (var cactusRequest in offer.RequestedCactuses)
                {
                    await _cactusOfferService.RemoveCactusRequest(cactusRequest.Id);
                }

                await _offerService.RemoveOffer(offerId);
                uow.Commit();

                return true;
            }
        }
    }
}
