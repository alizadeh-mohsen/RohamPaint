using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RohamPaint.ViewModels;

namespace RohamPaint.Pages.ColorPage.FormulPage
{
    public class CreateModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly IMapper _mapper;

        [BindProperty]
        public ColorFormulEditViewModel ColorFormul { get; set; } = new();


        public CreateModel(Data.ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult OnGet(int? ColorId)
        {
            ColorFormul.ColorId = ColorId ?? 0;
            ViewData["ColorID"] = new SelectList(_context.Color, "Id", "Id");
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var colorFormul = _mapper.Map<Models.ColorFormul>(ColorFormul);
            _context.ColorFormul.Add(colorFormul);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
