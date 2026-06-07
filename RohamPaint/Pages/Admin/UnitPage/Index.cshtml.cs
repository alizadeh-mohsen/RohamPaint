using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Models;

namespace RohamPaint.Pages.Admin.UnitPage
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public IndexModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Unit> Unit { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Unit = await _context.Unit.ToListAsync();
        }
    }
}
