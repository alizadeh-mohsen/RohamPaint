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

        public CreateModel(Data.ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            ViewData["BaseId"] = new SelectList(_context.Base, "Id", "Name");
            ViewData["CarId"] = new SelectList(_context.Car, "Id", "Make");
            ViewData["ColorTypeId"] = new SelectList(_context.ColorType, "Id", "Type");
            ViewData["UnitId"] = new SelectList(_context.Unit, "Id", "Name");
            ColorCreateViewModel.Formuls.Add(new Models.ColorFormul()); // Add an initial empty formul for the form
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

            var color = _mapper.Map<Models.Color>(ColorCreateViewModel);

            _context.Color.Add(color);
            await _context.SaveChangesAsync();

            foreach (var formul in ColorCreateViewModel.Formuls)
            {
                    var colorFormul = new  Models.ColorFormul
                    {
                        ColorID = color.Id,
                        BaseCode = formul.BaseCode,
                        Weight = formul.Weight
                    };


                    _context.ColorFormul.Add(formul);
                
            }

            _context.SaveChanges();


            return RedirectToPage("./Index");
        }

    }
}
