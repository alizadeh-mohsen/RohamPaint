using System.ComponentModel;

namespace RohamPaint.ViewModels
{
    public class ColorCreateViewModel
    {
        public int Id { get; set; }

        [DisplayName("Make")]
        public int CarId { get; set; }

        [DisplayName("Color Group")]
        public int ColorTypeId { get; set; }

        public string Code { get; set; } = null!;

        [DisplayName("Color Description")]
        public string? Comment { get; set; }

        [DisplayName("Unit")]
        public int? UnitId { get; set; }

        public string? Usage { get; set; }

        [DisplayName("Base")]
        public int? BaseId { get; set; }

        public List<ColorFormulViewModel> Formuls { get; set; } = new();
    }

}
