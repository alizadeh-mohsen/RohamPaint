using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace RohamPaint.Pages
{
    public class NotesModel : PageModel
    {
        private readonly IWebHostEnvironment _env;

        public NotesModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        [BindProperty]
        public string NoteText { get; set; } = string.Empty;

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

    }


}



