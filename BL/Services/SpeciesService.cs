using AutoMapper;
using BL.DTOs;
using CactusDAL.Models;
using Infrastructure;
using Infrastructure.Predicates;
using Infrastructure.Predicates.Operators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public class SpeciesService : ISpeciesService
    {
        private IMapper mapper;
        private QueryObject<SpeciesDto, Species> queryObject;
        private IRepository<Species> repository;

        public SpeciesService(IMapper mapper,
            IRepository<Species> repository,
            QueryObject<SpeciesDto, Species> queryObject
        )
        {
            this.mapper = mapper;
            this.repository = repository;
            this.queryObject = queryObject;
        }

        public async Task<IEnumerable<SpeciesDto>> GetAllAprovedSpecies()
        {
            IPredicate predicate = new SimplePredicate(nameof(Species.Approved), true, ValueComparingOperator.Equal);
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        }

        public async Task<IEnumerable<SpeciesDto>> getAllApprovedSpeciesWithGenus(int genusId)
        {
            IPredicate approvedPredicate = new SimplePredicate(nameof(Species.Approved), true, ValueComparingOperator.Equal);
            IPredicate genusPredicate = new SimplePredicate(nameof(Species.GenusId), genusId, ValueComparingOperator.Equal);
            IPredicate predicate = new CompositePredicate(new List<IPredicate> { approvedPredicate, genusPredicate }, LogicalOperator.AND);
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        }
        public async Task CreateSpecies(SpeciesCreateDto speciesCreateDto)
        {
            var species = mapper.Map<Species>(speciesCreateDto);
            await repository.Create(species);
        }

        public async Task ApproveSpecies(int speciesId)
        {
            var species = await repository.GetAsync(speciesId);
            species.Approved = true;
            repository.Update(species);
        }

        public async Task DeleteSpecies(int speciesId)
        {
            await repository.Delete(speciesId);
        }

        public async Task<IEnumerable<SpeciesDto>> getAllNewSpeciesProposals()
        {
            IPredicate predicate = new SimplePredicate(nameof(Species.Approved), false, ValueComparingOperator.Equal);
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        }

    }
}
