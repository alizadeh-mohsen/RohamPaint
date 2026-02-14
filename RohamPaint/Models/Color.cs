namespace RohamPaint.Models
{
    public class Color
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public int ColorTypeId { get; set; }

        public string Code { get; set; } = null!;

        public string? Comment { get; set; }

        public bool Lock { get; set; } = false;
        public DateTime LastUpdate { get; set; } = DateTime.Today;

        public int? UnitId { get; set; }

        public byte Accuracy { get; set; }
        public string? Usage { get; set; }

        public int? BaseId { get; set; }
        public virtual Base Base { get; set; }

        // Navigation properties
        public virtual Car Car { get; set; } = null!;

        public virtual ColorType ColorType { get; set; } = null!;

        public virtual Unit Unit { get; set; }

        public ICollection<ColorFormul> Formuls { get; set; }


    }

}
