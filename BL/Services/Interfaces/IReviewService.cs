using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IReviewService
    {
        public Task<IEnumerable<ReviewDto>> GetReviewsOnUser(int usedId);
        public Task<IEnumerable<ReviewDto>> GetReviewsOfTransfer(int transferId);
        public Task CreateReview(ReviewCreateDto reviewCreateDto);
    }
}
