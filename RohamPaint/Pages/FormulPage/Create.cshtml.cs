using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RohamPaint.ViewModels;

namespace RohamPaint.Pages.FormulPage
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
            ViewData["BaseId"] = new SelectList(_context.Base, "Id", "Name");
            ViewData["CarId"] = new SelectList(_context.Car, "Id", "Make");
            ViewData["ColorTypeId"] = new SelectList(_context.ColorType, "Id", "Type");
            ViewData["UnitId"] = new SelectList(_context.Unit, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public ColorCreateViewModel ColorCreateViewModel { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ColorCreateViewModel.Add(ColorCreateViewModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        public class ColorWeightItem
        {
            public string BaseColor { get; set; }
            public decimal Weight { get; set; }
        }

        public class ColorWeightViewModel
        {
            public List<ColorWeightItem> ColorWeights { get; set; } = new();
        }
    }
}
