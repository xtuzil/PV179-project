using BL.DTOs;
using BL.Exceptions;
using BL.Facades;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class UserController : WithUserInfoController
    {
        readonly IUserFacade _userFacade;
        readonly IAdministrationFacade _administrationFacade;
        readonly IUserCollectionFacade _userCollectionFacade;

        public static readonly string SKEY_REGISTERED = "_registered";
        public static readonly string SKEY_BANNED = "_banned";
        public static readonly string SKEY_MADE_ADMIN = "_madeAdmin";

        public UserController(IUserFacade userFacade,
            IAdministrationFacade administrationFacade,
            IUserCollectionFacade userCollectionFacade) : base(userFacade)
        {
            _userFacade = userFacade;
            _administrationFacade = administrationFacade;
            _userCollectionFacade = userCollectionFacade;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userFacade.GetAllUsers();

            return View(users);
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/register")]
        public async Task<IActionResult> Register(UserCreateDto user)
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

        [HttpGet]
        [Route("/login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/login")]
        public async Task<IActionResult> Login(UserLoginDto userLogin, string returnUrl)
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
            catch (BannedUserException)
            {
                TempData.Add(SKEY_BANNED, true);
                return View("Login");
            }
            catch (UnauthorizedAccessException)
            {
                ModelState.AddModelError("Password", "Invalid email or password.");
                return View("Login");
            }
        }

        [HttpGet]
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
        [HttpGet]
        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null || (User.Identity.Name != id.Value.ToString() && !User.IsInRole("Admin")))
            {
                return NotFound();
            }

            UserInfoDto user = await _userFacade.GetUserInfo(id.Value);

            return View(user);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditProfile(int? id)
        {
            if (id == null || (User.Identity.Name != id.Value.ToString() && !User.IsInRole("Admin")))
            {
                return NotFound();
            }

            UserInfoDto user = await _userFacade.GetUserInfo(id.Value);

            return View(new UserUpdateProfileDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditProfile(int id, [Bind("Id,FirstName,LastName,Email,PhoneNumber")] UserUpdateProfileDto user, IFormFile avatar)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (avatar != null && avatar.Length > 0)
                {
                    using var fileStream = avatar.OpenReadStream();
                    user.Avatar = new byte[avatar.Length];
                    fileStream.Read(user.Avatar, 0, (int)avatar.Length);
                }
                await _userFacade.UpdateUserInfo(user);
            }

            return RedirectToAction(nameof(Profile), new { id = user.Id });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ChangePassword(int? id)
        {
            if (id == null || (User.Identity.Name != id.Value.ToString() && !User.IsInRole("Admin")))
            {
                return NotFound();
            }

            UserInfoDto user = await _userFacade.GetUserInfo(id.Value);

            return View(new ChangePasswordDto
            {
                Id = user.Id,
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult ChangePassword(int id, [Bind("Id,Password,PasswordConfirmation")] ChangePasswordDto user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _userFacade.ChangePassword(user);
            }

            return RedirectToAction(nameof(Profile), new { id });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BanUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _administrationFacade.BlockUser(id.Value);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnbanUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _administrationFacade.UnblockUser(id.Value);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Collection(int? id, string? page)
        {
            if (id == null)
            {
                return NotFound();
            }
            int paginationPage = 1;
            if (page != null)
            {
                paginationPage = int.Parse(page);
            }
            var queryResult = await _userCollectionFacade.GetAllUserCactuses(id.Value);
            ViewBag.UserDetails = await _userFacade.GetUserInfo(id.Value);
            ViewData["Pagination"] = new PaginationViewModel(paginationPage, (int)queryResult.TotalItemsCount, queryResult.PageSize);
            return View(queryResult);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MakeAdmin(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userFacade.GetUserInfo(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            await _administrationFacade.MakeAdmin(id.Value);
            TempData.Add(SKEY_MADE_ADMIN, $"{user.FirstName} {user.LastName}");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> MakeAdminSecret(string secret)
        {
            var storedSecret = Environment.GetEnvironmentVariable("MAKE_ADMIN_SECRET");
            if (storedSecret == null || secret != storedSecret)
            {
                return NotFound();
            }

            var id = int.Parse(User.Identity.Name);
            await _administrationFacade.MakeAdmin(id);
            await HttpContext.SignOutAsync();
            await CreateClaimsAndSignInAsync(await _userFacade.GetUserInfo(id));

            return RedirectToAction(nameof(Index));
        }
    }
}
