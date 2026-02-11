namespace RohamPaint.Models
{
    public class Color
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public int ColorTypeId { get; set; }

        public string Code { get; set; } = null!;

        public string? Comment { get; set; }

        public bool Lock { get; set; }
        public DateTime LastUpdate { get; set; }

        public int? UnitId { get; set; }

        public short Accuracy { get; set; }
        public string? Usage { get; set; }

        public int? BaseId { get; set; }
        public virtual Base Base { get; set; }

        // Navigation properties
        public virtual Car Car { get; set; } = null!;

        public virtual ColorType ColorType { get; set; } = null!;

        public virtual Unit Unit { get; set; }


    }

}
