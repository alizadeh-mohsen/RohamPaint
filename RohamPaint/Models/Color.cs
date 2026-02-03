namespace RohamPaint.Models
{
    public class Color
    {
        public int ID { get; set; }

        public int CarID { get; set; }

        public int ColorTypeID { get; set; }

        public string Code { get; set; } = null!;

        public string? Comment { get; set; }

        public bool? Lock { get; set; }

        public DateTime? LastUpdate { get; set; }

        public int? UnitId { get; set; }

        public string? Usage { get; set; }

        public byte Accuracy { get; set; } = 0;

        public int? BaseId { get; set; }

        // Navigation properties
        public virtual Make Car { get; set; } = null!;

        public virtual ColorType ColorType { get; set; } = null!;

        public virtual Unit? Unit { get; set; }

        public virtual ICollection<Formul> Formuls { get; set; } = new List<Formul>();
    }

}
