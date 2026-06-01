using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using RohamPaint.ViewModels;

namespace RohamPaint.Pages.ColorPage
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        [BindProperty]
        public ColorDetailsViewModel Color { get; set; } = default!;

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
            TotalWeight = color.Formuls.Sum(f => f.Weight).ToString();

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




        public PartialViewResult OnPostMix(int formulId, int colorId, string weight, bool isGram)
        {
            var color = _context.Color
                .Include(c => c.Formuls)
                .FirstOrDefault(c => c.Id == colorId);
            List<ColorFormulViewModel> formuls = new List<ColorFormulViewModel>();


            if (string.IsNullOrWhiteSpace(weight))
                return Partial("_FormulsTable", formuls);

            var newValue = float.Parse(weight);
            var oldValue = color.Formuls.First(f => f.ID == formulId).Weight;
            var rate = newValue / oldValue;

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

            //return Partial("_FormulsTable", formuls);
            return Partial("_FormulsTable", new FormulsTableViewModel
            {
                Formuls = formuls
            });
        }
        public PartialViewResult OnPostChangeBaseWeight(int colorId, string cb_weight, bool cb_isGram)
        {
            var color = _context.Color
                .Include(c => c.Formuls)
                .FirstOrDefault(c => c.Id == colorId);
            List<ColorFormulViewModel> formuls = new List<ColorFormulViewModel>();

            if (string.IsNullOrWhiteSpace(cb_weight))
                return Partial("_FormulsTable", formuls);

            var newBase = float.Parse(cb_weight);
            var previousTotal = color.Formuls.Sum(f => f.Weight);

            var total = 0f;
            foreach (var formul in color.Formuls)
            {
                formul.Weight =
                     (float)(cb_isGram ?
                     Math.Round(formul.Weight * newBase / previousTotal, 1) :
                     Math.Round(formul.Weight * newBase / previousTotal, 2));
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

            //return Partial("_FormulsTable", formuls);
            return Partial("_FormulsTable", new FormulsTableViewModel
            {
                Formuls = formuls
            });
        }


        public void OnPostPrintFormul()
        {

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(50);
                    page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Times New Roman"));
                    var headerText = $"{Color.Car} - {Color.Code} - {Color.Base}";

                    page.Header().Row(row =>
                    {
                        row.RelativeItem();
                        row.RelativeItem(150).Text(headerText); // This adds your logo
                        row.RelativeItem();
                    });

                    page.Content().Column(column =>
                    {
                        column.Spacing(5);

                        // Company Name (Centered, Bold, Large)

                        column.Item().AlignCenter().Text("Roham-Paint.ir")
                            .FontSize(16)
                            .Bold();
                        column.Item().PaddingTop(15);
                        column.Item().Row(row =>
                        {
                            row.ConstantItem(60).Text("Base Core").SemiBold();
                            row.RelativeItem().Text("Weight").SemiBold();
                        });

                        column.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);


                        foreach (var item in Color.Formuls)
                        {
                            column.Item().Row(row =>
                            {
                                row.ConstantItem(70).Text(item.Weight.ToString());
                                row.RelativeItem().Text(item.BaseColor);
                            });
                        }
                        column.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);


                        var totalWeight = Color.Formuls.Sum(f => f.Weight).ToString();
                        column.Item().Row(row =>
                        {
                            row.ConstantItem(60).Text("Weight:").SemiBold(); // Fixed width for labels
                            row.RelativeItem().Text(totalWeight);
                        });

                        page.Footer().Row(row =>
                        {


                            row.ConstantItem(40).Text("Address:").SemiBold(); // Fixed width for labels
                            row.RelativeItem().Text("Iraj Sharifi");                // Value takes remaining space
                            row.ConstantItem(40).Text("Phone:").SemiBold(); // Fixed width for labels
                            row.RelativeItem().Text("09125031094");
                        });

                    });
                });
            });

            document.GeneratePdf();
        }

        public void OnPostPrintLabel()
        {

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(50);
                    page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Times New Roman"));
                    var headerText = $"{Color.Car} - {Color.Code} - {Color.Base}";

                    page.Header().Row(row =>
                    {
                        row.RelativeItem();
                        row.RelativeItem(150).Text(headerText); // This adds your logo
                        row.RelativeItem();
                    });

                    page.Content().Column(column =>
                    {
                        column.Spacing(5);

                        // Company Name (Centered, Bold, Large)

                        column.Item().AlignCenter().Text("Roham-Paint.ir")
                            .FontSize(16)
                            .Bold();
                        column.Item().PaddingTop(15);
                        column.Item().Row(row =>
                        {
                            row.ConstantItem(60).Text("Base Core").SemiBold();
                            row.RelativeItem().Text("Weight").SemiBold();
                        });

                        column.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);


                        foreach (var item in Color.Formuls)
                        {
                            column.Item().Row(row =>
                            {
                                row.ConstantItem(70).Text(item.Weight.ToString());
                                row.RelativeItem().Text(item.BaseColor);
                            });
                        }
                        column.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);


                        var totalWeight = Color.Formuls.Sum(f => f.Weight).ToString();
                        column.Item().Row(row =>
                        {
                            row.ConstantItem(60).Text("Weight:").SemiBold(); // Fixed width for labels
                            row.RelativeItem().Text(totalWeight);
                        });

                        page.Footer().Row(row =>
                        {


                            row.ConstantItem(40).Text("Address:").SemiBold(); // Fixed width for labels
                            row.RelativeItem().Text("Iraj Sharifi");                // Value takes remaining space
                            row.ConstantItem(40).Text("Phone:").SemiBold(); // Fixed width for labels
                            row.RelativeItem().Text("09125031094");
                        });

                    });
                });
            });

            document.GeneratePdf();
        }


    }
}
