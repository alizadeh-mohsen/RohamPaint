using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Data;
using RohamPaint.Modelhelper;
using RohamPaint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RohamPaint.Pages.FormulPage
{
    public class IndexModel : PageModel
    {
        private readonly RohamPaint.Data.ApplicationDbContext _context;

        public IndexModel(RohamPaint.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public MetaData MetaData { get; set; } = default!;

        public IList<Color> Colors { get; set; } = default!;

        public async Task OnGetAsync([FromQuery] QueryParams queryParams, string? search)

        {
            Colors = await _context.Color
                .Include(c => c.Car)
                .Include(c => c.ColorType)
                .Include(c => c.Unit).ToListAsync();


            var query = _context.Color.AsQueryable().AsNoTracking();
            query = query.Include(c => c.Car)
                .Include(c => c.ColorType)
                .Include(c => c.Unit);
            query = query.OrderBy(c => c.ID);
            if (!string.IsNullOrWhiteSpace(search))
            {
                string lowerSearch = search.ToLower();
                query = query.Where(c =>
                    (c.Code != null && c.Code.ToLower().Contains(lowerSearch))
                );
            }
            var colors = await PagedList<Color>.ToPagedList(query, queryParams.PageNumber, queryParams.PageSize);
            MetaData = colors.MetaData;
            Colors = colors;



        }
    }
}
