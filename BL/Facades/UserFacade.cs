﻿using BL.DTOs;
using BL.Services;
using CactusDAL;
using Infrastructure.EntityFramework;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class UserFacade : IUserFacade
    {
        private IUserService _userService;
        private IUnitOfWorkProvider _unitOfWorkProvider;

        public UserFacade(IUnitOfWorkProvider unitOfWorkProvider, IUserService userService)
        {
            _userService = userService;
            _unitOfWorkProvider = unitOfWorkProvider;
        }

        public async Task<List<UserInfoDto>> GetAllUserWithNameAsync(string name)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                return (List<UserInfoDto>) await _userService.GetUsersWithNameAsync(name);
            }
        }
    }
}