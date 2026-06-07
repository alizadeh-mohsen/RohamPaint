using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Models;

namespace RohamPaint.Pages.Admin.ColorPage
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public EditModel(Data.ApplicationDbContext context)
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
            Color = color;
            ViewData["BaseId"] = new SelectList(_context.Base.OrderBy(c => c.Name), "Id", "Name");
            ViewData["CarId"] = new SelectList(_context.Car.OrderBy(c => c.Name), "Id", "Name");
            ViewData["ColorTypeId"] = new SelectList(_context.ColorType.OrderBy(c => c.Type), "Id", "Type");
            ViewData["UnitId"] = new SelectList(_context.Unit.OrderBy(c => c.Name), "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Color).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorExists(Color.Id))
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

        private bool ColorExists(int id)
        {
            return _context.Color.Any(e => e.Id == id);
        }
    }
}
