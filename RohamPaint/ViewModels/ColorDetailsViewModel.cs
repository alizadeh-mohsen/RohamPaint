using System.ComponentModel;

namespace RohamPaint.ViewModels
{
    public class ColorDetailsViewModel
    {
        public int ID { get; set; }

        public string Code { get; set; } = null!;

        [DisplayName("Description")]
        public string? Comment { get; set; }

        public bool Lock { get; set; } = false;
        [DisplayName("Last Update")]
        public DateTime LastUpdate { get; set; }
        public string Unit { get; set; }
        public byte Accuracy { get; set; }
        public string Usage { get; set; }
        public string Base { get; set; }
        public string Car { get; set; }
        [DisplayName("Color Type")]
        public string ColorType { get; set; }
        public List<ColorFormulViewModel> Formuls { get; set; }
        public int TotalFormuls { get; set; }

    }

}

