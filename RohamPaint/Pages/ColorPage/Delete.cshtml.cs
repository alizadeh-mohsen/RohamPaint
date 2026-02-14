using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Models;

namespace RohamPaint.Pages.ColorPage
{
    public class DeleteModel : PageModel
    {
        private readonly RohamPaint.Data.ApplicationDbContext _context;

        public DeleteModel(RohamPaint.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Color Color { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _context.Color.FirstOrDefaultAsync(m => m.Id == id);

            if (color == null)
            {
                return NotFound();
            }
            else
            {
                Color = color;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _context.Color.FindAsync(id);
            if (color != null)
            {
                Color = color;
                _context.Color.Remove(Color);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
