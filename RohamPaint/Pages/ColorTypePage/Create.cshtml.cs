using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RohamPaint.Data;
using RohamPaint.Models;

namespace RohamPaint.Pages.ColorTypePage
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
            return Page();
        }

        [BindProperty]
        public ColorType ColorType { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ColorType.Add(ColorType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
