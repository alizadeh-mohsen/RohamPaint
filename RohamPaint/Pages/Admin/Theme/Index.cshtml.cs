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
        public string SelectedTheme { get; set; } = "Default";

        public List<SelectListItem> ThemeList { get; set; } = new();

        private readonly List<string> _themes = new()
        {
            "default", "cerulean", "cosmo", "cyborg", "darkly",
            "flatly", "journal", "litera", "lumen", "lux",
            "materia", "minty", "morph", "pulse", "quartz",
            "sandstone", "simplex", "sketchy", "slate", "solar",
            "spacelab", "superhero", "united", "vapor", "yeti", "zephyr"
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
                // Save the selected theme into a cookie that lasts for 1 year.
                // Use Request.IsHttps so the cookie is only marked Secure when running over HTTPS.
                // This avoids the cookie being ignored on production when the site isn't served over HTTPS.
                var option = new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    HttpOnly = true,
                    Secure = Request.IsHttps,
                    SameSite = SameSiteMode.Lax,
                    Path = "/"
                };

                Response.Cookies.Append("SelectedTheme", SelectedTheme, option);
            }

            PopulateThemeList();
            return RedirectToPage();
        }

        private void PopulateThemeList()
        {
            ThemeList.Clear();
            foreach (var theme in _themes)
            {
                ThemeList.Add(new SelectListItem
                {
                    Text = theme,
                    Value = theme,
                    Selected = string.Equals(theme, SelectedTheme, StringComparison.OrdinalIgnoreCase)
                });
            }
        }
    }
}