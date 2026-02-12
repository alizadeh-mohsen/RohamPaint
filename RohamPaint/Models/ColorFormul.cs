using System.Drawing;

namespace RohamPaint.Models
{
    public class ColorFormul
    {
        public int ID { get; set; }

        public int ColorID { get; set; }

        public int BaseCode { get; set; }

        public float Weight { get; set; }

        // Navigation properties
        public virtual Color Color { get; set; }

    }

}
