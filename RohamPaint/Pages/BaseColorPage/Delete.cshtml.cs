using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Data;
using RohamPaint.Models;

namespace RohamPaint.Pages.BaseColorPage
{
    public class DeleteModel : PageModel
    {
        private readonly RohamPaint.Data.ApplicationDbContext _context;

        public DeleteModel(RohamPaint.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BaseColor BaseColor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basecolor = await _context.BaseColor.FirstOrDefaultAsync(m => m.Id == id);

            if (basecolor == null)
            {
                return NotFound();
            }
            else
            {
                BaseColor = basecolor;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basecolor = await _context.BaseColor.FindAsync(id);
            if (basecolor != null)
            {
                BaseColor = basecolor;
                _context.BaseColor.Remove(BaseColor);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
