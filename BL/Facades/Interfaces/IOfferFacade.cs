using BL.DTOs;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface IOfferFacade
    {
        public Task<OfferDto> CreateOffer(OfferCreateDto offer);
        public Task AcceptOfferAsync(int offerId);
        public Task RejectOffer(int offerId);
        public Task<bool> RemoveOffer(int offerId);
        public Task<OfferDto> GetOffer(int OfferId);
    }
}
