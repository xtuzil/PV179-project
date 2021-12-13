﻿using BL.DTOs;
using BL.Facades;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        readonly IUserFacade _userFacade;

        public static readonly string SKEY_REGISTERED = "_registered";

        public UserController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(UserCreateDto user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            try
            {
                await _userFacade.CheckEmailNotInUse(user.Email);
                await _userFacade.RegisterUserAsync(user);
                TempData.Add(SKEY_REGISTERED, true);
                return RedirectToAction("Login", "User");
            }
            catch (Exception)
            {
                ModelState.AddModelError("Email", "An account with this email address already exists.");
                return View(user);
            }
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(UserLoginDto userLogin, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(userLogin);
            }

            try
            {
                var user = await _userFacade.LoginAsync(userLogin);

                await CreateClaimsAndSignInAsync(user);

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError("Password", "Invalid email or password.");
                return View("Login");
            }
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task CreateClaimsAndSignInAsync(UserInfoDto user)
        {
            var claims = new List<Claim>
            {
                //Set User Identity Name to actual user Id - easier access with user connected operations
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }

        [Authorize]
        public IActionResult Profile(int? id)
        {
            return View();
        }
    }
}
