using BL.DTOs;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface IOfferFacade
    {
        public Task<OfferDto> CreateOffer(OfferCreateDto offer);
        public Task<bool> AcceptOfferAsync(OfferDto offer);
        public void RejectOffer(int offerId);

        public Task<OfferDto> GetOffer(int OfferId);
    }
}
