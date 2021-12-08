using AutoMapper;
using BL.DTOs;
using CactusDAL.Models;
using Infrastructure;
using Infrastructure.Predicates;
using Infrastructure.Predicates.Operators;
using Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ReviewService : IReviewService
    {
        private IMapper mapper;
        private QueryObject<ReviewDto, Review> queryObject;
        private IUnitOfWorkProvider provider;
        private IRepository<Review> repository;

        public ReviewService(IUnitOfWorkProvider provider,
            IMapper mapper,
            IRepository<Review> repository,
            QueryObject<ReviewDto, Review> queryObject
        )
        {
            this.mapper = mapper;
            this.provider = provider;
            this.repository = repository;
            this.queryObject = queryObject;
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsOfTransfer(int transferId)
        {
            IPredicate predicate = new SimplePredicate(nameof(Review.TransferId), transferId, ValueComparingOperator.Equal);
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsOnUser(int usedId)
        {
            IPredicate predicate = new SimplePredicate(nameof(Review.UserId), usedId, ValueComparingOperator.Equal);
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        }

        public async Task CreateReview(ReviewCreateDto reviewCreateDto)
        {
            var review = mapper.Map<Review>(reviewCreateDto);
            await repository.Create(review);
        }


    }
}
