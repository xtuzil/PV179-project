using CactusDAL.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazorPages.Pages.Cactuses
{
    public class IndexModel : PageModel
    {
        private readonly CactusDAL.CactusDbContext _context;

        public IndexModel(CactusDAL.CactusDbContext context)
        {
            _context = context;
        }

        public IList<Cactus> Cactus { get; set; }

        public async Task OnGetAsync()
        {
            Cactus = await _context.Cactuses
                .Include(c => c.Owner)
                .Include(c => c.Species).ToListAsync();
        }
    }
}
