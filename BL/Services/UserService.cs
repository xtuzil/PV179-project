using AutoMapper;
using BL.Config;
using BL.DTOs;
using CactusDAL.Models;
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
    public class UserService : IUserService
    {
        private IMapper mapper;
        private QueryObject<UserInfoDto, User> queryObject;
        private IUnitOfWorkProvider provider;

        public UserService(IUnitOfWorkProvider provider)
        {
            mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
            this.provider = provider;
        }
        public async Task<IEnumerable<UserInfoDto>> GetUsersWithNameAsync(string name)
        {
            queryObject = new QueryObject<UserInfoDto, User>(mapper, provider);
            IPredicate predicate = new SimplePredicate(nameof(User.FirstName), name, ValueComparingOperator.Equal);
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        }

        

        

    }
}
