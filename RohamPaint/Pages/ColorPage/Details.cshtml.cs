using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Models;
using RohamPaint.ViewModels;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RohamPaint.Pages.ColorPage
{
    public class DetailsModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        [BindProperty]
        public ColorDetailsViewModel Color { get; set; } = default!;


        [BindProperty]
        public string TotalWeight { get; set; } = default!;

        //[BindProperty]
        //public List<ColorFormul> ColorFormuls { get; set; } = default!;

        public DetailsModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

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

        public class RequestModel
        {
            public string Id { get; set; }
            public string ColorId { get; set; }
            public string NewWeight { get; set; }
            public bool IsGram { get; set; }
        }


        //public JsonResult OnPostMix([FromBody] RequestModel data)
        //{
        //    int formulId = int.Parse(data.Id);
        //    int colorId = int.Parse(data.ColorId);

        //    var color = _context.Color.Include(c => c.Formuls).FirstOrDefault(c => c.Id == colorId);

        //    var total = 0f;
        //    if (string.IsNullOrEmpty(data.NewWeight.Trim()))
        //    {
        //        return new JsonResult(new { ok = true });
        //    }
        //    var newValue = float.Parse(data.NewWeight);
        //    var oldValue = color.Formuls.FirstOrDefault(c => c.ID == formulId).Weight;
        //    var rate = newValue / oldValue;
        //    List<ColorFormulViewModel> formuls = new List<ColorFormulViewModel>();
        //    foreach (var formul in color.Formuls)
        //    {
        //        if (formul.ID == formulId)
        //        {
        //            formul.Weight = newValue;
        //        }
        //        else
        //        {
        //            formul.Weight = (float)(data.IsGram ?
        //                Math.Round(formul.Weight * rate, 1) :
        //                Math.Round(formul.Weight * rate, 2));
        //        }

        //        formuls.Add(new ColorFormulViewModel
        //        {
        //            Id = formul.ID,
        //            BaseColor = formul.BaseColor,
        //            Weight = formul.Weight
        //        });


        //        total += formul.Weight;
        //    }
        //    Color.Formuls = formuls;
        //    TotalWeight = total.ToString();
        //    return

        //    //for (int i = 0; i < ColorFormuls.Count; i++)
        //    //{
        //    //    lstWeight.Items[i] = lblUnit.Text.ToLower().Contains("gr") ?
        //    //        Math.Round(float.Parse(lstWeight.Items[i].ToString()) * rate, 1) :
        //    //        Math.Round(float.Parse(lstWeight.Items[i].ToString()) * rate, 2);
        //    //    total += float.Parse(lstWeight.Items[i].ToString());
        //    //}
        //    //lblTotal.Text = string.Format(Helper.NumberFormatInfo, total);
        //}

        public PartialViewResult OnPostMix(int formulId, int colorId, string weight, bool isGram)
        {
            var color = _context.Color
                .Include(c => c.Formuls)
                .FirstOrDefault(c => c.Id == colorId);

            if (string.IsNullOrWhiteSpace(weight))
                return Partial("_FormulsTable", MapToViewModel(color.Formuls));

            var newValue = float.Parse(weight);
            var oldValue = color.Formuls.First(f => f.ID == formulId).Weight;
            var rate = newValue / oldValue;

            List<ColorFormulViewModel> formuls = new List<ColorFormulViewModel>();
            var total = 0f;
            foreach (var formul in color.Formuls)
            {
                formul.Weight = formul.ID == formulId
                    ? newValue
                    : (float)(isGram ? Math.Round(formul.Weight * rate, 1)
                                      : Math.Round(formul.Weight * rate, 2));
                formuls.Add(new ColorFormulViewModel
                {
                    Id = formul.ID,
                    BaseColor = formul.BaseColor,
                    Weight = formul.Weight
                });
                total += formul.Weight;

            }
            Color.Formuls = formuls;
            TotalWeight = total.ToString();
            // Return just the partial — htmx swaps it into the page
            return Partial("_FormulsTable", MapToViewModel(color.Formuls));
        }

        private List<ColorFormulViewModel> MapToViewModel(IEnumerable<ColorFormul> formuls) =>
            formuls.Select(f => new ColorFormulViewModel
            {
                Id = f.ID,
                BaseColor = f.BaseColor,
                Weight = f.Weight
            }).ToList();
    }
}
