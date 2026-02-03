using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Modelhelper;
using RohamPaint.Models;

namespace RohamPaint.Pages.BaseColorPage
{
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public IndexModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<BaseColor> BaseColor { get;set; } = default!;
        public MetaData MetaData { get; set; } = default!;

        public async Task OnGetAsync([FromQuery] QueryParams queryParams, string? search)
        {
            var query = _context.BaseColor.AsQueryable().AsNoTracking();
            if (!string.IsNullOrWhiteSpace(search))
            {
                string lowerSearch = search.ToLower();
                query = query.Where(c =>
                    (c.Code != null && c.Code.ToLower().Contains(lowerSearch)) 
                );
            }
            var customers = await PagedList<BaseColor>.ToPagedList(query, queryParams.PageNumber, queryParams.PageSize);
            MetaData = customers.MetaData;
            BaseColor = customers;
        }
    }
}
