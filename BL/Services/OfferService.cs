using AutoMapper;
using BL.DTOs;
using BL.Exceptions;
using CactusDAL.Models;
using Infrastructure;
using Infrastructure.Predicates;
using Infrastructure.Predicates.Operators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public class OfferService : IOfferService
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
            var offer = await repository.GetAsync(offerId);
            return mapper.Map<OfferDto>(offer); ;
        }

        public async Task<OfferDto> AcceptOffer(int offerId)
        {
            var offer = await repository.GetAsync(offerId);
            if ((offer.Author.AccountBalance - (double)offer.OfferedMoney < 0) ||
                    (offer.Recipient.AccountBalance - (double)offer.RequestedMoney < 0))
            {
                throw new InsufficientMoneyException();
            }

            offer.Response = OfferStatus.Accepted;
            offer.ResponseDate = DateTime.UtcNow;

            repository.Update(offer);

            return mapper.Map<OfferDto>(offer);
        }

        public async Task<Offer> CreateOffer(OfferCreateDto offerDto)
        {
            var offer = mapper.Map<Offer>(offerDto);
            offer.Response = OfferStatus.Created;
            offer.ResponseDate = DateTime.UtcNow;
            await repository.Create(offer);
            return offer;
        }

        public async Task<IEnumerable<OfferDto>> GetTransferedOffersOfUser(int userId)
        {
            IPredicate authorPredicate = new SimplePredicate(nameof(Offer.AuthorId), userId, ValueComparingOperator.Equal);
            IPredicate recipientPredicate = new SimplePredicate(nameof(Offer.RecipientId), userId, ValueComparingOperator.Equal);
            IPredicate containUserIdPredicate = new CompositePredicate(new List<IPredicate> { authorPredicate, recipientPredicate }, LogicalOperator.OR);

            IPredicate trasferedPredicate = new SimplePredicate(nameof(Offer.Response), OfferStatus.Transfered, ValueComparingOperator.Equal);

            IPredicate compositePredicate = new CompositePredicate(new List<IPredicate> { containUserIdPredicate, trasferedPredicate }, LogicalOperator.AND);
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = compositePredicate, SortAscending = true })).Items;
        }

        public async Task<OfferDto> UpdateOfferStatus(int offerId, OfferStatus status)
        {
            var offer = await repository.GetAsync(offerId);
            offer.Response = status;
            offer.ResponseDate = DateTime.UtcNow;
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

        public async Task RemoveOffer(int offerId)
        {
            await repository.Delete(offerId);
        }

    }
}
