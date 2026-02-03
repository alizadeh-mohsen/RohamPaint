using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Data;
using RohamPaint.Models;

namespace RohamPaint.Pages.CarPage
{
    public class DetailsModel : PageModel
    {
        private readonly RohamPaint.Data.ApplicationDbContext _context;

        public DetailsModel(RohamPaint.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Make Make { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var make = await _context.Make.FirstOrDefaultAsync(m => m.Id == id);
            if (make == null)
            {
                return NotFound();
            }
            else
            {
                Make = make;
            }
            return Page();
        }
    }
}
