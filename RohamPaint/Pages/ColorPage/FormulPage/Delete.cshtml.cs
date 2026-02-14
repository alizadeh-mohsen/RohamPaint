using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Data;
using RohamPaint.Models;

namespace RohamPaint.Pages.ColorPage.FormulPage
{
    public class DeleteModel : PageModel
    {
        private readonly RohamPaint.Data.ApplicationDbContext _context;

        public DeleteModel(RohamPaint.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ColorFormul ColorFormul { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colorformul = await _context.ColorFormul.FirstOrDefaultAsync(m => m.ID == id);

            if (colorformul == null)
            {
                return NotFound();
            }
            else
            {
                ColorFormul = colorformul;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colorformul = await _context.ColorFormul.FindAsync(id);
            if (colorformul != null)
            {
                ColorFormul = colorformul;
                _context.ColorFormul.Remove(ColorFormul);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
