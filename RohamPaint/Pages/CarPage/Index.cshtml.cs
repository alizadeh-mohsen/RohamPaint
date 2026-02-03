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
    public class IndexModel : PageModel
    {
        private readonly RohamPaint.Data.ApplicationDbContext _context;

        public IndexModel(RohamPaint.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Make> Make { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Make = await _context.Make.ToListAsync();
        }
    }
}
