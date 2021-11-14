using AutoMapper;
using BL.DTOs;
using CactusDAL.Models;
using Infrastructure;
using Infrastructure.Predicates;
using Infrastructure.Predicates.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    class OfferService : IOfferService
    {
        private IMapper mapper;
        private IRepository<Offer> repository;
        private QueryObject<OfferDto, Offer> queryObject;

        public OfferService(IMapper mapper, IRepository<Offer> repository, QueryObject<OfferDto, Offer> queryObject)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.queryObject = queryObject;
        }

        public async Task<OfferDto> GetOffer(int offerId)
        {
            var offer = await repository.GetAsync(offerId,
                offer => offer.Author,
                offer => offer.Recipient,
                offer => offer.CactusOffers,
                offer => offer.CactusRequests
            );

            return mapper.Map<OfferDto>(offer);
        }

        public async Task<OfferDto> AcceptOffer(int offerId)
        {
            var offer = await repository.GetAsync(offerId);
            offer.Response = OfferStatus.Accepted;
            repository.Update(offer);

            return mapper.Map<OfferDto>(offer);
        }

        public OfferDto CreateOffer(OfferCreateDto offerDto)
        {
            var offer = mapper.Map<Offer>(offerDto);
            repository.Create(offer);

            return mapper.Map<OfferDto>(offer);
        }

        public OfferDto CreateCounterOffer(OfferCreateDto offerDto, int previousOffer)
        {
            var offer = mapper.Map<Offer>(offerDto);
            offer.PreviousOfferId = previousOffer;
            repository.Create(offer);

            return mapper.Map<OfferDto>(offer);
        }

        public async Task<OfferDto> RejectOffer(int offerId)
        {
            var offer = await repository.GetAsync(offerId);
            offer.Response = OfferStatus.Declined;
            repository.Update(offer);

            return mapper.Map<OfferDto>(offer);
        }

        public async Task<IEnumerable<OfferDto>> GetAuthoredOffersForUser(int userId)
        {
            IPredicate predicate = new SimplePredicate(nameof(Offer.AuthorId), userId, ValueComparingOperator.Equal);
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        }

        public async Task<IEnumerable<OfferDto>> GetReceivedOffersForUser(int userId)
        {
            IPredicate predicate = new SimplePredicate(nameof(Offer.RecipientId), userId, ValueComparingOperator.Equal);
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        }
    }
}
