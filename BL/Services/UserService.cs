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
    public class UserService : IUserService
    {
        private IMapper mapper;
        private QueryObject<UserInfoDto, User> queryObject;
        private IUnitOfWorkProvider provider;
        private IRepository<User> repository;

        public UserService(IUnitOfWorkProvider provider,
            IMapper mapper,
            IRepository<User> repository,
            QueryObject<UserInfoDto, User> queryObject
        )
        {
            this.mapper = mapper;
            this.provider = provider;
            this.repository = repository;
            this.queryObject = queryObject;
        }

        public async Task<UserInfoDto> GetUserInfo(int userId)
        {
            return mapper.Map<UserInfoDto>(await repository.GetAsync(userId));

        }

        public async Task<IEnumerable<UserInfoDto>> GetUsersWithNameAsync(string name)
        {
            IPredicate predicate = new SimplePredicate(nameof(User.FirstName), name, ValueComparingOperator.Equal);
            return (await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true })).Items;
        }

        public void UpdateUserInfo(UserUpdateDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            repository.Update(user);
        }
    }
}
