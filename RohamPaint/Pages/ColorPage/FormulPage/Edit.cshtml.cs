using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Data;
using RohamPaint.ViewModels;

namespace RohamPaint.Pages.ColorPage.FormulPage
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EditModel(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [BindProperty]
        public ColorFormulEditViewModel ColorFormul { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colorformul = await _context.ColorFormul.FindAsync(id);
            if (colorformul == null)
            {
                return NotFound();
            }
            ColorFormul = _mapper.Map<ColorFormulEditViewModel>(colorformul);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var colorFormulToUpdate = await _context.ColorFormul.FindAsync(ColorFormul.Id);

            if (colorFormulToUpdate == null)
            {
                return NotFound();
            }

            colorFormulToUpdate.BaseColor = ColorFormul.BaseColor;
            colorFormulToUpdate.Weight = ColorFormul.Weight;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorFormulExists(ColorFormul.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Details");
        }

        private bool ColorFormulExists(int id)
        {
            return _context.ColorFormul.Any(e => e.ID == id);
        }
    }
}
