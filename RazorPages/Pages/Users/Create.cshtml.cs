using CactusDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace RazorPages.Pages.Users
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
            ViewData["AddressId"] = new SelectList(_context.PostalAddresses, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public User User { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
