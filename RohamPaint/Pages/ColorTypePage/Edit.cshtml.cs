using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Data;
using RohamPaint.Models;

namespace RohamPaint.Pages.ColorTypePage
{
    public class EditModel : PageModel
    {
        private readonly RohamPaint.Data.ApplicationDbContext _context;

        public EditModel(RohamPaint.Data.ApplicationDbContext context)
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

            var colortype =  await _context.ColorType.FirstOrDefaultAsync(m => m.Id == id);
            if (colortype == null)
            {
                return NotFound();
            }
            ColorType = colortype;
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

            _context.Attach(ColorType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorTypeExists(ColorType.Id))
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

        private bool ColorTypeExists(int id)
        {
            return _context.ColorType.Any(e => e.Id == id);
        }
    }
}
