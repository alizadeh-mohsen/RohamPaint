using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Models;

namespace RohamPaint.Pages.CarPage
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public DetailsModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Car Make { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var make = await _context.Car.FirstOrDefaultAsync(m => m.Id == id);
            if (make == null)
            {
                return NotFound();
            }
            else
            {
                Make = make;
            }
            return Page();
        }
    }
}
