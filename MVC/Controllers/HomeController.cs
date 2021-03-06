using BL.Facades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Models;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class HomeController : WithUserInfoController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUserFacade userFacade) : base(userFacade)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("terms-and-conditions")]
        public IActionResult Terms()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
