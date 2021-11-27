﻿using BL.DTOs;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface IOfferFacade
    {
        public OfferDto CreateOffer(OfferCreateDto offer);
        public OfferDto CreateCounterOffer(OfferCreateDto offer, int previousOfferId);
        public Task<bool> AcceptOfferAsync(OfferDto offer);
        public void RejectOffer(int offerId);

        public Task<OfferDto> GetOffer(int OfferId);
    }
}
