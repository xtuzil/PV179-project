using BL.Facades;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class SpeciesController : Controller
    {
        private readonly IUserCollectionFacade _facade;

        public SpeciesController(IUserCollectionFacade facade)
        {
            _facade = facade;
        }

        public async Task<IActionResult> Json(int? id)
        {
            if (id == null)
            {
                return Json(new object[] { });
            }

            var species = await _facade.GetAllApprovedSpeciesWithGenus((int)id);
            return Json(species);
        }
    }
}
