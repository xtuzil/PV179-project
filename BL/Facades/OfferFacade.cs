using AutoMapper;
using BL.Config;
using BL.DTOs;
using BL.Services;
using BL.Services.Interfaces;
using Infrastructure.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class OfferFacade : IOfferFacade
    {
        private IUnitOfWorkProvider unitOfWorkProvider;
        private IOfferService offerService;
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
            this.offerService = offerService;
            _userService = userService;
            _cactusService = cactusService;
            _cactusOfferService = cactusOfferService;
            _transferService = transferService;
        }

        public async Task<bool> AcceptOfferAsync(OfferDto offer)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                await _cactusService.UpdateCactusAmountAsync(1, -20);

                var newcCatus = _cactusService.CreateNewCactusInstanceForTransfer(offer.OfferedCactuses[0].Cactus, 30);

                await offerService.AcceptOffer(offer.Id);
               
                if ((offer.Author.AccountBalance - (double)offer.OfferedMoney < 0) ||
                    (offer.Recipient.AccountBalance - (double)offer.RequestedMoney < 0)) {
                    return false;
                }

                // remove offered money from each user
                await _userService.RemoveUserMoneyAsync(offer.Author.Id, (double)offer.OfferedMoney);
                await _userService.RemoveUserMoneyAsync(offer.Recipient.Id, (double)offer.RequestedMoney);
               
                // remove offer cactuses from each user
                foreach (var cactusOffer in offer.OfferedCactuses)
                {
                    if (cactusOffer.Cactus.Amount - cactusOffer.Amount <= 0)
                    {
                        _cactusService.RemoveCactusFromUser(cactusOffer.Cactus);
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
                        _cactusService.RemoveCactusFromUser(cactusRequest.Cactus);
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
                _transferService.CreateTransfer(offer.Id);

                uow.Commit();
                return true;
            }
        }

        public OfferDto CreateCounterOffer(OfferCreateDto offer, int previousOfferId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                var offerDto = offerService.CreateCounterOffer(offer, previousOfferId);
                uow.Commit();
                return offerDto;
            }
        }

        public OfferDto CreateOffer(OfferCreateDto offer)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                var createdOffer = offerService.CreateOffer(offer);
                //Console.WriteLine($"In create offer: Offer with iD: {createdOffer.Id} with offered money: {createdOffer.OfferedMoney} and author Id: {createdOffer.Author.Id}");
                uow.Commit();
                foreach (var cactusOffered in offer.OfferedCactuses)
                {
                    cactusOffered.OfferId = createdOffer.Id;
                    _cactusOfferService.AddCactusOffer(cactusOffered);
                }
                foreach (var cactusRequested in offer.RequestedCactuses)
                {
                    cactusRequested.OfferId = createdOffer.Id;
                    _cactusOfferService.AddCactusRequest(cactusRequested); 
                }
                uow.Commit();

                //TODO:
                var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
                return mapper.Map<OfferDto>(createdOffer);
            }
        }

        public async Task<OfferDto> GetOffer(int offerId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return await offerService.GetOffer(offerId);
            }
        }

        public void RejectOffer(int offerId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                offerService.RejectOffer(offerId);
                uow.Commit();
            }
        }
    }
}
