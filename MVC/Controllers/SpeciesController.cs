using BL.DTOs;
using BL.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Authorize]
    public class SpeciesController : Controller
    {
        private readonly IUserCollectionFacade _userCollectionFacade;
        private readonly ICactusFacade _cactusFacade;

        public SpeciesController(IUserCollectionFacade userCollectionFacade, ICactusFacade cactusFacade)
        {
            _userCollectionFacade = userCollectionFacade;
            _cactusFacade = cactusFacade;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewData["GenusId"] = new SelectList(await _userCollectionFacade.GetAllGenuses(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Name,LatinName,GenusId")] SpeciesCreateDto species)
        {
            if (ModelState.IsValid)
            {
                await _cactusFacade.ProposeNewSpecies(species);

                return RedirectToAction("Index", "Home");
            }

            ViewData["GenusId"] = new SelectList(await _userCollectionFacade.GetAllGenuses(), "Id", "Name", species.GenusId);
            return View(species);
        }

        public async Task<IActionResult> Json(int? id)
        {
            if (id == null)
            {
                return Json(new object[] { });
            }

            var species = await _userCollectionFacade.GetAllApprovedSpeciesWithGenus((int)id);
            return Json(species);
        }
    }
}
