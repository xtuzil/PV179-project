using AutoMapper;
using BL.DTOs;
using CactusDAL.Models;
using Infrastructure;
using Infrastructure.Predicates;
using Infrastructure.Predicates.Operators;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BL.Services
{
    public class UserService : IUserService
    {
        private IMapper mapper;
        private QueryObject<UserInfoDto, User> queryObject;
        private IUnitOfWorkProvider provider;
        private IRepository<User> repository;

        private const int PBKDF2IterCount = 100000;
        private const int PBKDF2SubkeyLength = 160 / 8;
        private const int saltSize = 128 / 8;

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

        public async Task<UserInfoDto> GetUserWithEmail(string email)
        {
            IPredicate predicate = new SimplePredicate(nameof(User.Email), email, ValueComparingOperator.Equal);
            var result = await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate });
            if (result.TotalItemsCount != 1)
            {
                return null;
            }
            return result.Items.First();
        }

        public void UpdateUserInfo(UserUpdateDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            repository.Update(user);
        }

        public async Task<UserInfoDto> AuthorizeUserAsync(UserLoginDto login)
        {
            var userDto = await GetUserWithEmail(login.Email);
            if (userDto == null)
            {
                return null;
            }

            //get user entity
            var user = await repository.GetAsync(userDto.Id);

            var (hash, salt) = user != null ? GetPassAndSalt(user.Password) : (string.Empty, string.Empty);

            var succ = user != null && VerifyHashedPassword(hash, salt, login.Password);
            return succ ? userDto : null;
        }

        public async Task RegisterUser(UserCreateDto user)
        {
            var (hash, salt) = CreateHash(user.Password);
            user.Password = string.Join(',', hash, salt);

            var mapped = mapper.Map<User>(user);
            await repository.Create(mapped);
        }

        private (string, string) GetPassAndSalt(string passwordHash)
        {
            var result = passwordHash.Split(',');
            if (result.Count() != 2)
            {
                return (string.Empty, string.Empty);
            }
            return (result[0], result[1]);
        }

        private bool VerifyHashedPassword(string hashedPassword, string salt, string password)
        {
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            var saltBytes = Convert.FromBase64String(salt);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, PBKDF2IterCount))
            {
                var generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                return hashedPasswordBytes.SequenceEqual(generatedSubkey);
            }
        }

        private Tuple<string, string> CreateHash(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                return Tuple.Create(Convert.ToBase64String(subkey), Convert.ToBase64String(salt));
            }
        }
    }
}
