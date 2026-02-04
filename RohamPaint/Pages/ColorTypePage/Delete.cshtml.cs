using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Data;
using RohamPaint.Models;

namespace RohamPaint.Pages.ColorTypePage
{
    public class DeleteModel : PageModel
    {
        private readonly RohamPaint.Data.ApplicationDbContext _context;

        public DeleteModel(RohamPaint.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colortype = await _context.ColorType.FindAsync(id);
            if (colortype != null)
            {
                ColorType = colortype;
                _context.ColorType.Remove(ColorType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
