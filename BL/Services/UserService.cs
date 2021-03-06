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

        public async Task<IEnumerable<UserInfoDto>> GetAll()
        {
            var users = await repository.GetAll();
            return mapper.Map<IEnumerable<UserInfoDto>>(users);
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

        public async Task UpdateUserInfo(UserUpdateProfileDto userDto)
        {
            var user = await repository.GetAsync(userDto.Id);
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            if (userDto.Avatar != null)
            {
                user.Avatar = userDto.Avatar;
            }
            repository.Update(user);
        }

        public async Task ChangePassword(ChangePasswordDto userDto)
        {
            var (hash, salt) = CreateHash(userDto.Password);
            var user = await repository.GetAsync(userDto.Id);
            user.Password = string.Join(',', hash, salt);
            repository.Update(user);
        }

        public async Task CreateUser(UserCreateDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            user.AccountBalance = 100;
            await repository.Create(user);
        }

        public async Task RemoveUserMoneyAsync(int userId, double amount)
        {
            var user = await repository.GetAsync(userId);
            user.AccountBalance -= amount;
            repository.Update(user);
        }

        public async Task AddUserMoneyAsync(int userId, double amount)
        {
            var user = await repository.GetAsync(userId);
            user.AccountBalance += amount;
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

            await CreateUser(user);
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

        public async Task BanUser(int userId, bool ban)
        {
            var user = await repository.GetAsync(userId);
            user.Banned = ban;
            repository.Update(user);
        }

        public async Task MakeAdmin(int userId)
        {
            var user = await repository.GetAsync(userId);
            user.Role = Role.Admin;
            repository.Update(user);
        }
    }
}
