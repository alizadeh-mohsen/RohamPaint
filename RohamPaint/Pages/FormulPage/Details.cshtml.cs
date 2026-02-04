using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Data;
using RohamPaint.Models;

namespace RohamPaint.Pages.FormulPage
{
    public class DetailsModel : PageModel
    {
        private readonly RohamPaint.Data.ApplicationDbContext _context;

        public DetailsModel(RohamPaint.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Color Color { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _context.Color.FirstOrDefaultAsync(m => m.ID == id);
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
    }
}
