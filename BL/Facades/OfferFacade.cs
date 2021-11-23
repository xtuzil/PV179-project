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

        public async Task AcceptOfferAsync(OfferDto offer)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                await offerService.AcceptOffer(offer.Id);

               
                // remove offered money from each user
                await _userService.RemoveUserMoneyAsync(offer.Author.Id, (double)offer.OfferedMoney);
                await _userService.RemoveUserMoneyAsync(offer.Recipient.Id, (double)offer.RequestedMoney);

               
                /* @Fuyune uncomment this part of method to get error
                // remove offer cactuses from each user
                foreach (var cactusOffer in offer.OfferedCactuses)
                {
                    if (cactusOffer.Cactus.Amount - cactusOffer.Amount <= 0)
                    {
                        _cactusService.RemoveCactusFromUser(cactusOffer.Cactus);
                    } 
                    else
                    {
                        // remove amount of sender from user
                        cactusOffer.Cactus.Amount -= cactusOffer.Amount;
                        _cactusService.UpdateCactusInformation(cactusOffer.Cactus);

                        // create new instance of cactus for transfer
                        cactusOffer.Cactus = _cactusService.CreateNewCactusInstanceForTransfer(cactusOffer.Cactus, cactusOffer.Amount);
                         
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
                        // remove amount of sender from user
                        cactusRequest.Cactus.Amount -= cactusRequest.Amount;
                        _cactusService.UpdateCactusInformation(cactusRequest.Cactus);

                        // create new instance of cactus for transfer
                        cactusRequest.Cactus = _cactusService.CreateNewCactusInstanceForTransfer(cactusRequest.Cactus, cactusRequest.Amount);
                    }

                }

                // create Transfer object in db
                _transferService.CreateTransfer(offer.Id);

                uow.Commit();
                */
            }
        }

        public OfferDto CreateCounterOffer(OfferCreateDto offer, int previousOfferId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return offerService.CreateCounterOffer(offer, previousOfferId);
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
            }
        }
    }
}
