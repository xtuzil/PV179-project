using BL.DTOs;
using BL.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Authorize]
    public class SpeciesController : WithUserInfoController
    {
        private readonly IUserCollectionFacade _userCollectionFacade;
        private readonly ICactusFacade _cactusFacade;
        private readonly IAdministrationFacade _administrationFacade;

        public SpeciesController(
            IUserCollectionFacade userCollectionFacade,
            ICactusFacade cactusFacade,
            IAdministrationFacade administrationFacade,
            IUserFacade userFacade) : base(userFacade)
        {
            _userCollectionFacade = userCollectionFacade;
            _cactusFacade = cactusFacade;
            _administrationFacade = administrationFacade;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PendingProposals()
        {
            return View(await _administrationFacade.GetAllPendingRequestsForNewSpecies());
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var approvedSpecies = await _cactusFacade.GetAllApprovedSpecies();
            var pendingSpecies = await _cactusFacade.GetAllPendingSpecies();
            var rejectedSpecies = await _cactusFacade.GetAllRejectedSpecies();
            ViewBag.ApprovedSpecies = approvedSpecies;
            ViewBag.PendingSpecies = pendingSpecies;
            ViewBag.RejectedSpecies = rejectedSpecies;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _administrationFacade.ApproveSpecies(id.Value);
            }

            return RedirectToAction(nameof(PendingProposals));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reject(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _administrationFacade.RejectSpecies(id.Value);
            }

            return RedirectToAction(nameof(PendingProposals));
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

                return RedirectToAction("Index");
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
