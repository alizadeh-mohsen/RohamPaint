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

namespace RohamPaint.Pages.ColorTypePage
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public MetaData MetaData { get; set; } = default!;

        public IList<ColorType> ColorType { get;set; } = default!;

        public async Task OnGetAsync([FromQuery] QueryParams queryParams, string? search)
        {
            var query = _context.ColorType.AsQueryable().AsNoTracking();
            query = query.OrderBy(c => c.Type);
            if (!string.IsNullOrWhiteSpace(search))
            {
                string lowerSearch = search.ToLower();
                query = query.Where(c =>
                    (c.Type != null && c.Type.ToLower().Contains(lowerSearch))
                );
            }
            var customers = await PagedList<ColorType>.ToPagedList(query, queryParams.PageNumber, queryParams.PageSize);
            MetaData = customers.MetaData;
            ColorType = customers;
        }
    }
}
