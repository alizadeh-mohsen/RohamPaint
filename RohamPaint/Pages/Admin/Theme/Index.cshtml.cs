using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace RohamPaint.Pages.Admin.Theme
{
    [Authorize]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string SelectedTheme { get; set; } = "default";

        public List<SelectListItem> ThemeList { get; set; } = new();

        private readonly List<string> _themes = new()
        {
            "Default", "Cerulean", "Cosmo", "Cyborg", "Darkly",
            "Flatly", "Journal", "Litera", "Lumen", "Lux",
            "Materia", "Minty", "Morph", "Pulse", "Quartz",
            "Sandstone", "Simplex", "Sketchy", "Slate", "Solar",
            "Spacelab", "Superhero", "United", "Vapor", "Yeti", "Zephyr"
        };

        public void OnGet()
        {
            // Fetch the current theme from the cookie, default to 'Default' if not found
            SelectedTheme = Request.Cookies["SelectedTheme"] ?? "Default";
            PopulateThemeList();
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrEmpty(SelectedTheme))
            {
                // Save the selected theme into a cookie that lasts for 1 year
                CookieOptions option = new()
                {
                    Expires = DateTime.Now.AddYears(1),
                    HttpOnly = true,
                    Secure = true, // Ensures cookie is transmitted over HTTPS
                    SameSite = SameSiteMode.Strict
                };

                Response.Cookies.Append("SelectedTheme", SelectedTheme, option);
            }

            PopulateThemeList();
            return RedirectToPage();
        }

        private void PopulateThemeList()
        {
            foreach (var theme in _themes)
            {
                ThemeList.Add(new SelectListItem
                {
                    Text = theme,
                    Value = theme,
                    Selected = theme == SelectedTheme
                });
            }
        }
    }
}