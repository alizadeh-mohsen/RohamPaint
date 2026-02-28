using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Models;

namespace RohamPaint.Pages.ColorTypePage
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly RohamPaint.Data.ApplicationDbContext _context;

        public DetailsModel(RohamPaint.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ColorType ColorType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colortype = await _context.ColorType.FirstOrDefaultAsync(m => m.Id == id);
            if (colortype == null)
            {
                return NotFound();
            }
            else
            {
                ColorType = colortype;
            }
            return Page();
        }
    }
}
