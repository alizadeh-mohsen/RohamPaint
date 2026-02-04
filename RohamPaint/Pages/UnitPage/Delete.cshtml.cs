using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Data;
using RohamPaint.Models;

namespace RohamPaint.Pages.UnitPage
{
    public class DeleteModel : PageModel
    {
        private readonly RohamPaint.Data.ApplicationDbContext _context;

        public DeleteModel(RohamPaint.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Unit Unit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Unit.FirstOrDefaultAsync(m => m.Id == id);

            if (unit == null)
            {
                return NotFound();
            }
            else
            {
                Unit = unit;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Unit.FindAsync(id);
            if (unit != null)
            {
                Unit = unit;
                _context.Unit.Remove(Unit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
