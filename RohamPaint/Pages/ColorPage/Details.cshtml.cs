using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Models;
using RohamPaint.ViewModels;

namespace RohamPaint.Pages.ColorPage
{
    public class DetailsModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public DetailsModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ColorDetailsViewModel Color { get; set; } = default!;
        public List<ColorFormul> ColorFormuls { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? ID)
        {
            if (ID == null)
            {
                return NotFound();
            }

            var color = await _context.Color.AsNoTracking().Include(c => c.Formuls)
                  .Where(m => m.Id == ID)
                  .Select(c => new ColorDetailsViewModel
                  {
                      ID = c.Id,
                      Code = c.Code,
                      Comment = c.Comment,
                      Lock = c.Lock,
                      LastUpdate = c.LastUpdate,
                      Unit = c.Unit.Name,
                      Accuracy = c.Accuracy,
                      Usage = c.Usage ?? "",
                      Base = c.Base.Name,
                      Car = c.Car.Name,
                      ColorType = c.ColorType.Type,
                      TotalFormuls = c.Formuls.Count(),
                      Formuls = c.Formuls.Select(f => new ColorFormulViewModel
                      {
                          Id = f.ID,
                          BaseColor = f.BaseColor,
                          Weight = f.Weight

                      }).OrderBy(f => f.BaseColor).ToList()
                  }).FirstOrDefaultAsync();


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
