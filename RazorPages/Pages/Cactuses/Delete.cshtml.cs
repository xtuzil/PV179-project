using CactusDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace RazorPages.Pages.Cactuses
{
    public class DeleteModel : PageModel
    {
        private readonly CactusDAL.CactusDbContext _context;

        public DeleteModel(CactusDAL.CactusDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cactus Cactus { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cactus = await _context.Cactuses
                .Include(c => c.Owner)
                .Include(c => c.Species).FirstOrDefaultAsync(m => m.Id == id);

            if (Cactus == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cactus = await _context.Cactuses.FindAsync(id);

            if (Cactus != null)
            {
                _context.Cactuses.Remove(Cactus);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
