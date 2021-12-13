using BL.DTOs;
using BL.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private readonly IUserCollectionFacade _facade;

        public CollectionController(IUserCollectionFacade facade)
        {
            _facade = facade;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _facade.GetAllUserCactuses(new UserInfoDto { Id = int.Parse(User.Identity.Name) }));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cactus = await _facade.GetCactus((int)id);
            if (cactus == null)
            {
                return NotFound();
            }

            return View(cactus);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewData["GenusId"] = new SelectList(await _facade.GetAllGenuses(), "Id", "Name");
            return View();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("GenusId,SpeciesId,ForSale,SowingDate,PotSize,Amount,Note,Id")] CactusCreateDto cactus)
        {
            if (ModelState.IsValid)
            {
                cactus.OwnerId = int.Parse(User.Identity.Name);
                _facade.AddCactusToCollection(cactus);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenusId"] = new SelectList(await _facade.GetAllGenuses(), "Id", "Name", cactus.GenusId);
            ViewData["SpeciesId"] = new SelectList(await _facade.GetAllApprovedSpeciesWithGenus(cactus.GenusId), "Id", "Name", cactus.SpeciesId);
            return View(cactus);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cactus = await _facade.GetCactus(id.Value);
            if (cactus == null)
            {
                return NotFound();
            }
            ViewData["GenusId"] = new SelectList(await _facade.GetAllGenuses(), "Id", "Name", cactus.Species.Genus.Id);
            ViewData["SpeciesId"] = new SelectList(await _facade.GetAllApprovedSpeciesWithGenus(cactus.Species.Genus.Id), "Id", "Name", cactus);
            return View(cactus);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Species,ForSale,SowingDate,PotSize,Amount,Note,Id")] CactusDto cactus)
        {
            if (id != cactus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                cactus.Owner = new UserInfoDto { Id = int.Parse(User.Identity.Name) };
                _facade.UpdateCactusInformation(cactus);
                return RedirectToAction(nameof(Details), new { id = cactus.Id });
            }
            ViewData["GenusId"] = new SelectList(await _facade.GetAllGenuses(), "Id", "Name", cactus.Species.Genus.Id);
            ViewData["SpeciesId"] = new SelectList(await _facade.GetAllApprovedSpeciesWithGenus(cactus.Species.Genus.Id), "Id", "Name", cactus);
            return View(cactus);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cactus = await _facade.GetCactus(id.Value);
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
            var cactus = await _facade.GetCactus(id);
            _facade.RemoveCactus(cactus);
            return RedirectToAction(nameof(Index));
        }
    }
}
