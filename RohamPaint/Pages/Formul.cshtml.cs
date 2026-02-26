using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Modelhelper;
using RohamPaint.ViewModels;

namespace RohamPaint.Pages
{
    public class FormulModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public FormulModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public MetaData MetaData { get; set; }

        public IList<ColorViewModel> Colors { get; set; }

        public async Task OnGetAsync([FromQuery] QueryParams queryParams, string? search)
        {
            try
            {
                var query = _context.Color.AsNoTracking()
                    .Select(c => new ColorViewModel
                    {
                        ID = c.Id,
                        Code = c.Code,
                        Make = c.Car.Name,
                        ColorType = c.ColorType.Type,
                        Comment = c.Comment,
                        LastUpdate = c.LastUpdate,
                        Unit = c.Unit.Name
                    });

                query = query.OrderBy(c => c.Code);

                if (!string.IsNullOrWhiteSpace(search))
                {
                    string lowerSearch = search.ToLower();
                    query = query.Where(c =>
                        (c.Code != null && c.Code.ToLower().Contains(lowerSearch) ||
                        c.Comment != null && c.Comment.ToLower().Contains(lowerSearch) ||
                        c.Make != null && c.Make.ToLower().Contains(lowerSearch))
                    );
                }

                var colors = await PagedList<ColorViewModel>.ToPagedList(query, queryParams.PageNumber, queryParams.PageSize);
                MetaData = colors.MetaData;
                Colors = colors;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // HTMX handler: returns only the partial list (no full page refresh)
        public async Task<PartialViewResult> OnGetColorsAsync([FromQuery] QueryParams queryParams, string? search)
        {
            var query = _context.Color.AsNoTracking()
                .Select(c => new ColorViewModel
                {
                    ID = c.Id,
                    Code = c.Code,
                    Make = c.Car.Name,
                    ColorType = c.ColorType.Type,
                    Comment = c.Comment,
                    LastUpdate = c.LastUpdate,
                    Unit = c.Unit.Name
                });
            query = query.OrderBy(c => c.Code);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var lowerSearch = search.ToLower();
                query = query.Where(c =>
                    (c.Code != null && c.Code.ToLower().Contains(lowerSearch) ||
                     c.Comment != null && c.Comment.ToLower().Contains(lowerSearch) ||
                     c.Make != null && c.Make.ToLower().Contains(lowerSearch))
                );
            }

            var colors = await PagedList<ColorViewModel>.ToPagedList(query, queryParams.PageNumber, queryParams.PageSize);

            // Return the partial that renders the list + pagination.
            return Partial("_ColorsList", colors);
        }
    }
}
