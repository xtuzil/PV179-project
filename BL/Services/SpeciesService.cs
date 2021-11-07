using AutoMapper;
using BL.Config;
using BL.DTOs;
using CactusDAL.Models;
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
    public class SpeciesService
    {
        private IMapper mapper;
        private QueryObject<SpeciesDto, Species> queryObject;
        private IUnitOfWorkProvider provider;

        public SpeciesService(IUnitOfWorkProvider provider)
        {
            mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
            this.provider = provider;
        }

        public async Task<IEnumerable<SpeciesDto>> GetAllAprovedSpecies()
        {
            queryObject = new QueryObject<SpeciesDto, Species>(mapper, provider);
            IPredicate predicate = new SimplePredicate(nameof(Species.Approved), true, ValueComparingOperator.Equal);
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        }

        public async Task<IEnumerable<SpeciesDto>> getAllApprovedSpeciesWithGenus(GenusDto genusdto)
        {
            var genus = mapper.Map<Genus>(genusdto);
            queryObject = new QueryObject<SpeciesDto, Species>(mapper, provider);
            IPredicate approvedPredicate = new SimplePredicate(nameof(Species.Approved), true, ValueComparingOperator.Equal);
            IPredicate genusPredicate = new SimplePredicate(nameof(Species.Genus), genus, ValueComparingOperator.Equal);
            IPredicate predicate = new CompositePredicate(new List<IPredicate> { approvedPredicate, genusPredicate }, LogicalOperator.AND);
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        }





    }
}
