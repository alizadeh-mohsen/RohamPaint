using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RohamPaint.Data;
using RohamPaint.Models;

namespace RohamPaint.Pages.FormulPage
{
    public class CreateModel : PageModel
    {
        private readonly RohamPaint.Data.ApplicationDbContext _context;

        public CreateModel(RohamPaint.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CarID"] = new SelectList(_context.Car, "Id", "Make");
        ViewData["ColorTypeID"] = new SelectList(_context.ColorType, "Id", "Type");
        ViewData["UnitId"] = new SelectList(_context.Unit, "Id", "Name");
        ViewData["BaseId"] = new SelectList(_context.Base, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public ColorViewModel Color { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Color.Add(Color);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
