using CactusDAL.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazorPages.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly CactusDAL.CactusDbContext _context;

        public IndexModel(CactusDAL.CactusDbContext context)
        {
            _context = context;
        }

        public IList<User> User { get; set; }

        public async Task OnGetAsync()
        {
            User = await _context.Users
                .Include(u => u.Address).ToListAsync();
        }
    }
}
