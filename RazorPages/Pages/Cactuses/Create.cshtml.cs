using CactusDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace RazorPages.Pages.Cactuses
{
    public class CreateModel : PageModel
    {
        private readonly CactusDAL.CactusDbContext _context;

        public CreateModel(CactusDAL.CactusDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["SpeciesId"] = new SelectList(_context.Species, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Cactus Cactus { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Cactuses.Add(Cactus);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
