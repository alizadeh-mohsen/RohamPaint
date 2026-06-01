using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace RohamPaint.Pages.NotePage
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _env;

        public IndexModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        [BindProperty]
        public string NoteText { get; set; } = string.Empty;

        public string StatusMessage => TempData["StatusMessage"] as string ?? string.Empty;

        private string NoteFilePath => Path.Combine(_env.ContentRootPath, "Data", "Note.txt");

        public async Task<IActionResult> OnGetAsync()
        {
            if (!System.IO.File.Exists(NoteFilePath))
            {
                // Ensure file exists with an empty content so editor can show it.
                await System.IO.File.WriteAllTextAsync(NoteFilePath, string.Empty, Encoding.UTF8);
            }

            NoteText = await System.IO.File.ReadAllTextAsync(NoteFilePath, Encoding.UTF8);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Optionally validate length or other constraints here.
            await System.IO.File.WriteAllTextAsync(NoteFilePath, NoteText ?? string.Empty, Encoding.UTF8);

            TempData["StatusMessage"] = "Note.txt saved successfully.";
            // PRG to avoid duplicate post on refresh
            return RedirectToPage();
        }
    }
}
