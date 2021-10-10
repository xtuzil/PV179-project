using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CactusDAL;
using CactusDAL.Models;

namespace RazorPages.Pages.Cactuses
{
    public class DetailsModel : PageModel
    {
        private readonly CactusDAL.CactusDbContext _context;

        public DetailsModel(CactusDAL.CactusDbContext context)
        {
            _context = context;
        }

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
    }
}
