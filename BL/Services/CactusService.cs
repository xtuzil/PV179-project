using AutoMapper;
using BL.DTOs;
using CactusDAL.Models;
using Infrastructure;
using Infrastructure.Predicates;
using Infrastructure.Predicates.Operators;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<CactusDto>> GetCactusesLike(string name)
        {
            IPredicate predicate = new SimplePredicate(nameof(Cactus.Name), name, ValueComparingOperator.StringContains);
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        }

        public async Task<IEnumerable<CactusDto>> GetCactusesOlderThan(int age)
        {
            var maxSowingDate = DateTime.Today.AddYears(-age);
            IPredicate predicate = new SimplePredicate(nameof(Cactus.SowingDate), maxSowingDate, ValueComparingOperator.LessThanOrEqual);
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        }

        public async Task<IEnumerable<CactusDto>> GetCactusesWithSpecies(int speciesId)
        {
            IPredicate predicate = new SimplePredicate(nameof(Cactus.SpeciesId), speciesId, ValueComparingOperator.Equal);
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        }

        public async Task<IEnumerable<CactusDto>> GetCactusesWithSpecies(IEnumerable<SpeciesDto> speciesList)
        {
            var speciesPredicates = new List<IPredicate>();
            foreach (SpeciesDto species in speciesList)
            {
                speciesPredicates.Add(new SimplePredicate(nameof(Cactus.SpeciesId), species.Id, ValueComparingOperator.Equal));
            }
            IPredicate predicate = new CompositePredicate(speciesPredicates, LogicalOperator.OR);
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
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

        public async Task<CactusDto> TransferCactus(int userId, int cactusId)
        {
            var cactus = await repository.GetAsync(cactusId);
            cactus.OwnerId = userId;
            repository.Update(cactus);

            return mapper.Map<CactusDto>(cactus);
        }
    }
}
