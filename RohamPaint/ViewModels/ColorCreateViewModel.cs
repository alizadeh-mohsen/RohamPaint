using RohamPaint.Models;
using System.ComponentModel;

namespace RohamPaint.ViewModels
{
    public class ColorCreateViewModel
    {
        //public ColorCreateViewModel()
        //{
        //    Formul = new Dictionary<string, float>();
        //}

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
        public List<Formul> Formuls { get; set; } = new List<Formul>();

        //public virtual Base Base { get; set; }
        //public virtual Car Car { get; set; } = null!;
        //public virtual ColorType ColorType { get; set; } = null!;
        //public virtual Unit Unit { get; set; }
        //public Dictionary<string, float> Formul { get; set; }

    }

}
