using AutoMapper;
using BL.Config;
using BL.DTOs;
using BL.Services;
using BL.Services.Interfaces;
using CactusDAL.Models;
using Infrastructure.UnitOfWork;
using System;
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

        public async Task AcceptOfferAsync(int offerId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                var offer = await _offerService.AcceptOffer(offerId);

                // remove offered money from each user
                await _userService.RemoveUserMoneyAsync(offer.Author.Id, (double)offer.OfferedMoney);
                await _userService.RemoveUserMoneyAsync(offer.Recipient.Id, (double)offer.RequestedMoney);
               
                // remove offer cactuses from each user
                foreach (var cactusOffer in offer.OfferedCactuses)
                {
                    if (cactusOffer.Cactus.Amount - cactusOffer.Amount <= 0)
                    {
                        await _cactusService.RemoveCactusFromUser(cactusOffer.Cactus.Id);
                    } 
                    else
                    {
                        // remove amount of cactuses from user
                        await _cactusService.UpdateCactusAmountAsync(cactusOffer.Cactus.Id, -cactusOffer.Amount);

                        // create new instance of cactus for transfer
                        // set the new instance to actual cactuseOffer
                        var newCatus =  _cactusService.CreateNewCactusInstanceForTransfer(cactusOffer.Cactus, cactusOffer.Amount);
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
                        var newCatus = _cactusService.CreateNewCactusInstanceForTransfer(cactusRequest.Cactus, cactusRequest.Amount);
                        uow.Commit();
                        await _cactusOfferService.UpdateCactusRequestCactusAsync(cactusRequest.Id, newCatus.Id);
                    }
                }

                // create Transfer object in db
                await _transferService.CreateTransfer(offer.Id);

                uow.Commit();
            }
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
                foreach (var cactusOffered in offer.OfferedCactuses)
                {
                    await _cactusOfferService.AddCactusOffer(createdOffer.Id, cactusOffered.Key, cactusOffered.Value);
                }
                foreach (var cactusRequested in offer.RequestedCactuses)
                {
                    await _cactusOfferService.AddCactusRequest(createdOffer.Id, cactusRequested.Key, cactusRequested.Value);
                }
                uow.Commit();

                //TODO: We might do not want to mapping in Facade, but for now it is necessary because of retrieving id
                var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
                return mapper.Map<OfferDto>(createdOffer);
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

                await _offerService.RemoveOffer(offerId);
                uow.Commit();

                return true;
            }
        }
    }
}
