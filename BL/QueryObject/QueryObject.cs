using AutoMapper;
using BL.DTOs;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;
using System.Threading.Tasks;

namespace BL
{
    public class QueryObject<TDto, TEntity> where TEntity : class
    {
        private IMapper mapper;

        private IQuery<TEntity> query;

        public QueryObject(IMapper mapper, IUnitOfWorkProvider provider, IQuery<TEntity> query)
        {
            this.mapper = mapper;
            this.query = query;
        }

        public async Task<QueryResultDto<TDto>> ExecuteQueryAsync(FilterDto filter)
        {
            query.Where(filter.Predicate);
            if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
            {
                query.SortBy(filter.SortCriteria, filter.SortAscending);
            }
            if (filter.RequestedPageNumber.HasValue)
            {
                query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }
            var queryResult = await query.ExecuteAsync();

            var queryResultDto = mapper.Map<QueryResultDto<TDto>>(queryResult);
            //var queryResultDtoEntiy = mapper.Map<QueryResultDto<TEntity>>(queryResult);
            //var queryResultDto = mapper.Map<QueryResultDto<TDto>>(queryResultDtoEntiy);
            return queryResultDto;
        }

    }
}
