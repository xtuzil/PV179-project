using AutoMapper;
using BL.Config;
using BL.DTOs;
using CactusDAL.Models;
using Infrastructure;
using Infrastructure.EntityFramework;
using Infrastructure.Predicates;
using Infrastructure.Predicates.Operators;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class CactusService : ICactusService
    {
        private IMapper mapper;
        private IUnitOfWorkProvider provider;
        private IRepository<Cactus> repository;
        private QueryObject<CactusDto, Cactus> queryObject;

        public CactusService(IUnitOfWorkProvider provider,
            IMapper mapper,
            IRepository<Cactus> repository,
            QueryObject<CactusDto, Cactus> queryObject
        )
        {
            this.mapper = mapper;
            this.provider = provider;
            this.repository = repository;
            this.queryObject = queryObject;
        }
        public async Task<IEnumerable<CactusDto>> GetAllUserCactuses(UserInfoDto userInfoDto)
        {
            var user = mapper.Map<User>(userInfoDto);

            IPredicate predicate = new SimplePredicate(nameof(Cactus.Owner), user, ValueComparingOperator.Equal);
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        }

        public async Task<IEnumerable<CactusDto>> GetUserCactusesForSale(UserInfoDto userInfoDto)
        {
            var user = mapper.Map<User>(userInfoDto);
            IPredicate userPredicate = new SimplePredicate(nameof(Cactus.Owner), user, ValueComparingOperator.Equal);
            IPredicate forSalePredicate = new SimplePredicate(nameof(Cactus.ForSale), true, ValueComparingOperator.Equal);
            IPredicate predicate = new CompositePredicate(new List<IPredicate> { userPredicate, forSalePredicate }, LogicalOperator.AND);
            
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        }

        public void AddCactus(CactusCreateDto cactusDto)
        {
            var cactus = mapper.Map<Cactus>(cactusDto);
            cactus.ForSale = true;
            repository.Create(cactus);
        }

        public void UpdateCactusInformation(CactusDto cactusDto)
        {
            var cactus = mapper.Map<Cactus>(cactusDto);
            repository.Update(cactus);
        }

        public void RemoveCactus(CactusDto cactusDto)
        {
            var cactus = mapper.Map<Cactus>(cactusDto);
            repository.Delete(cactus);
        }
    }
}
