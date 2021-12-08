using BL.DTOs;
using CactusDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IOfferService
    {
        public Task<OfferDto> GetOffer(int offerId);
        public Offer CreateOffer(OfferCreateDto offerDto);
        public Task<OfferDto> AcceptOffer(int offerId);
        public Task<OfferDto> UpdateOfferStatus(int offerId, OfferStatus status);
        public Task<IEnumerable<OfferDto>> GetAuthoredOffersForUser(int userId);
        public Task<IEnumerable<OfferDto>> GetReceivedOffersForUser(int userId);
    }
}
