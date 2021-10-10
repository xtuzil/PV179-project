using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CactusDAL;
using CactusDAL.Models;

namespace RazorPages.Pages.Cactuses
{
    public class EditModel : PageModel
    {
        private readonly CactusDAL.CactusDbContext _context;

        public EditModel(CactusDAL.CactusDbContext context)
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
           ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
           ViewData["SpeciesId"] = new SelectList(_context.Species, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Cactus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CactusExists(Cactus.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CactusExists(int id)
        {
            return _context.Cactuses.Any(e => e.Id == id);
        }
    }
}
