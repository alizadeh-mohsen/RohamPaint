using System.Drawing;

namespace RohamPaint.Models
{
    public class Formul
    {
        public int ID { get; set; }

        public int ColorID { get; set; }

        public int BaseID { get; set; }

        public float Weight { get; set; }

        // Navigation properties
        public virtual Color Color { get; set; }

        public virtual BaseColor BaseColor { get; set; }
    }

}
