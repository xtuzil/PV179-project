using BL.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class WithUserInfoController : Controller
    {
        private readonly IUserFacade myUserFacade;

        public WithUserInfoController(IUserFacade userFacade)
        {
            myUserFacade = userFacade;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.User = await myUserFacade.GetUserInfo(int.Parse(User.Identity.Name));
            }
            await next();
        }
    }
}
