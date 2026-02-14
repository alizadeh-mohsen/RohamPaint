using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RohamPaint.ViewModels;

namespace RohamPaint.Pages.FormulPage
{
    public class CreateModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly IMapper _mapper;


        [BindProperty]
        public ColorCreateViewModel ColorCreateViewModel { get; set; } = new();

        public CreateModel(Data.ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            ColorCreateViewModel = new ColorCreateViewModel(); // Add this line
        }

        public IActionResult OnGet()
        {
            ViewData["BaseId"] = new SelectList(_context.Base, "Id", "Name");
            ViewData["CarId"] = new SelectList(_context.Car, "Id", "Name");
            ViewData["ColorTypeId"] = new SelectList(_context.ColorType, "Id", "Type");
            ViewData["UnitId"] = new SelectList(_context.Unit, "Id", "Name");
            ColorCreateViewModel.Formuls.Add(new());
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var color = _mapper.Map<Models.Color>(ColorCreateViewModel);

                _context.Color.Add(color);
                await _context.SaveChangesAsync();

                foreach (var formul in ColorCreateViewModel.Formuls)
                {
                    var colorFormul = new Models.ColorFormul
                    {
                        ColorID = color.Id,
                        BaseCode = formul.BaseCode,
                        Weight = formul.Weight
                    };

                    _context.ColorFormul.Add(colorFormul);

                }

                _context.SaveChanges();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
