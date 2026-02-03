using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Data;
using RohamPaint.Modelhelper;
using RohamPaint.Models;

namespace RohamPaint.Pages.BasePage
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Base> Base { get;set; } = default!;

        public MetaData MetaData { get; set; } = default!;

        public async Task OnGetAsync([FromQuery] QueryParams queryParams, string? search)
        {
            var query = _context.Base.AsQueryable().AsNoTracking();
            if (!string.IsNullOrWhiteSpace(search))
            {
                string lowerSearch = search.ToLower();
                query = query.Where(c =>
                    (c.Name != null && c.Name.ToLower().Contains(lowerSearch))
                );
            }
            var customers = await PagedList<Base>.ToPagedList(query, queryParams.PageNumber, queryParams.PageSize);
            MetaData = customers.MetaData;
            Base = customers;
        }
    }
}
