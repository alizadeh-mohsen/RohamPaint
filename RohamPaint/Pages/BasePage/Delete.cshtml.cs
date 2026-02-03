using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Data;
using RohamPaint.Models;

namespace RohamPaint.Pages.BasePage
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Base Base { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baseFromDb = await _context.Base.FirstOrDefaultAsync(m => m.Id == id);

            if (baseFromDb == null)
            {
                return NotFound();
            }
            else
            {
                Base = baseFromDb;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baseFromDb = await _context.Base.FindAsync(id);
            if (baseFromDb != null)
            {
                Base = baseFromDb;
                _context.Base.Remove(Base);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
