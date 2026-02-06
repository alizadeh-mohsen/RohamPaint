using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RohamPaint.Models
{
    public class ColorViewModel
    {
        public int ID { get; set; }
        
        [DisplayName("Make")]
        public int CarID { get; set; }

        [DisplayName("Color Group")]
        public int ColorTypeID { get; set; }

        [DisplayName("Color Code")]
        public string Code { get; set; } = null!;
        
        [DisplayName("Color Description")]
        public string? Comment { get; set; }

        [DisplayName("Unit")]
        public int? UnitId { get; set; }

        public string? Usage { get; set; }

        [DisplayName("Base")]
        public int? BaseId { get; set; }

        public Dictionary<string,float> Formul { get; set; }

        // Navigation properties
        public virtual Car Car { get; set; } = null!;

        public virtual ColorType ColorType { get; set; } = null!;

        public virtual Unit? Unit { get; set; }

        public virtual ICollection<Formul> Formuls { get; set; } = new List<Formul>();
    }

}
