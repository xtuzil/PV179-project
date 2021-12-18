using BL.DTOs;
using BL.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Authorize]
    public class CollectionController : WithUserInfoController
    {
        private readonly IUserCollectionFacade _userCollectionFacade;
        private readonly ICactusFacade _cactusFacade;

        public CollectionController(IUserCollectionFacade userCollectionFacade, ICactusFacade cactusFacade, IUserFacade userFacade) : base(userFacade)
        {
            _userCollectionFacade = userCollectionFacade;
            _cactusFacade = cactusFacade;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userCollectionFacade.GetAllUserCactuses(int.Parse(User.Identity.Name)));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cactus = await _userCollectionFacade.GetCactus((int)id);
            if (cactus == null)
            {
                return NotFound();
            }

            return View(cactus);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewData["GenusId"] = new SelectList(await _userCollectionFacade.GetAllGenuses(), "Id", "Name");
            return View();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("GenusId,SpeciesId,ForSale,SowingDate,PotSize,Amount,Note,Id")] CactusCreateDto cactus, IFormFile photo)
        {
            var species = await _cactusFacade.GetSpecies(cactus.SpeciesId);
            if (species == null || species.Genus.Id != cactus.GenusId)
            {
                ModelState.AddModelError("SpeciesId", "Invalid species.");
            }

            if (ModelState.IsValid)
            {
                if (photo != null && photo.Length > 0)
                {
                    using var fileStream = photo.OpenReadStream();
                    cactus.Image = new byte[photo.Length];
                    fileStream.Read(cactus.Image, 0, (int)photo.Length);
                }
                cactus.OwnerId = int.Parse(User.Identity.Name);
                await _userCollectionFacade.AddCactusToCollection(cactus);
                return RedirectToAction(nameof(Index));
            }

            ViewData["GenusId"] = new SelectList(await _userCollectionFacade.GetAllGenuses(), "Id", "Name", cactus.GenusId);
            ViewData["SpeciesId"] = new SelectList(await _userCollectionFacade.GetAllApprovedSpeciesWithGenus(cactus.GenusId), "Id", "Name", cactus.SpeciesId);
            return View(cactus);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cactus = await _userCollectionFacade.GetCactus(id.Value);
            if (cactus == null)
            {
                return NotFound();
            }

            var updateDto = new CactusUpdateDto()
            {
                Id = cactus.Id,
                Amount = cactus.Amount,
                ForSale = cactus.ForSale,
                Note = cactus.Note,
                OwnerId = cactus.Owner.Id,
                PotSize = cactus.PotSize,
                SowingDate = cactus.SowingDate,
                Species = cactus.Species,
                Image = cactus.Image,
            };

            return View(updateDto);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Species,ForSale,SowingDate,PotSize,Amount,Note,Id")] CactusUpdateDto cactus, IFormFile photo)
        {
            if (id != cactus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (photo != null && photo.Length > 0)
                {
                    using var fileStream = photo.OpenReadStream();
                    cactus.Image = new byte[photo.Length];
                    fileStream.Read(cactus.Image, 0, (int)photo.Length);
                }
                cactus.OwnerId = int.Parse(User.Identity.Name);
                await _userCollectionFacade.UpdateCactusInformation(cactus);
                return RedirectToAction(nameof(Details), new { id = cactus.Id });
            }

            return View(cactus);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cactus = await _userCollectionFacade.GetCactus(id.Value);
            if (cactus == null)
            {
                return NotFound();
            }

            return View(cactus);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userCollectionFacade.RemoveCactus(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
