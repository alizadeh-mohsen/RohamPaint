using System.ComponentModel;

namespace RohamPaint.ViewModels
{
    public class ColorViewModel
    {
        public int ID { get; set; }

        public string Code { get; set; } = null!;

        public string Make { get; set; }

        [DisplayName("Color Type")]
        public string ColorType { get; set; }

        [DisplayName("Color Description")]
        public string? Comment { get; set; }

        [DisplayName("Last Update")]
        public DateTime LastUpdate { get; set; }

        public string Usage { get; set; }

        public string Unit { get; set; }
    }

}
