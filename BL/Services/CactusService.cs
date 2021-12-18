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

        //public async Task<IEnumerable<CactusDto>> GetCactusesLike(string name)
        //{
        //    IPredicate predicate = new SimplePredicate(nameof(Cactus.Name), name, ValueComparingOperator.StringContains);
        //    return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        //}

        public async Task<CactusDto> GetCactus(int id)
        {
            return mapper.Map<CactusDto>(await repository.GetAsync(id));
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

        public async Task<IEnumerable<CactusDto>> GetAllUserCactuses(int userId)
        {
            IPredicate predicate = new SimplePredicate(nameof(Cactus.OwnerId), userId, ValueComparingOperator.Equal);
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        }

        public async Task<IEnumerable<CactusDto>> GetUserCactusesForSale(int userId)
        {
            IPredicate userPredicate = new SimplePredicate(nameof(Cactus.OwnerId), userId, ValueComparingOperator.Equal);
            IPredicate forSalePredicate = new SimplePredicate(nameof(Cactus.ForSale), true, ValueComparingOperator.Equal);
            IPredicate predicate = new CompositePredicate(new List<IPredicate> { userPredicate, forSalePredicate }, LogicalOperator.AND);

            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        }

        public async Task AddCactus(CactusCreateDto cactusDto)
        {
            var cactus = mapper.Map<Cactus>(cactusDto);
            await repository.Create(cactus);
        }

        public async Task UpdateCactusInformation(CactusUpdateDto cactusDto)
        {
            var cactus = await repository.GetAsync(cactusDto.Id);
            cactus.Note = cactusDto.Note;
            cactus.OwnerId = cactusDto.OwnerId;
            cactus.SpeciesId = cactusDto.Species.Id;
            cactus.PotSize = cactusDto.PotSize;
            cactus.ForSale = cactusDto.ForSale;
            cactus.Amount = cactusDto.Amount;
            cactus.SowingDate = cactusDto.SowingDate;

            if (cactusDto.Image != null)
            {
                cactus.Image = cactusDto.Image;
            }
            repository.Update(cactus);
        }

        public async Task UpdateCactusAmountAsync(int cactusId, int amount)
        {
            
            var cactus = await repository.GetAsync(cactusId);
            cactus.Amount += amount;
            repository.Update(cactus);
        }

        public async Task UpdateCactusOwnerAsync(int cactusId, int userId)
        {

            var cactus = await repository.GetAsync(cactusId);
            cactus.OwnerId = userId;
            repository.Update(cactus);
        }

        public async Task RemoveCactus(int cactusId)
        {
            await repository.Delete(cactusId);
        }

        public async Task RemoveCactusFromUser(int cactusId)
        {
            var cactus = await repository.GetAsync(cactusId);
            cactus.OwnerId = null;
            repository.Update(cactus);
        }

        public async Task<Cactus> CreateNewCactusInstanceForTransfer(CactusDto cactusCreateDto, int amount)
        {
            var createDto = mapper.Map<CactusCreateDto>(cactusCreateDto);
            var cactus = mapper.Map<Cactus>(createDto);
            cactus.OwnerId = null;
            cactus.Amount = amount;
            await repository.Create(cactus);
            return cactus;
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
