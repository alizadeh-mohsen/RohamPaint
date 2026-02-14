namespace RohamPaint.Models
{
    public class ColorFormul
    {
        public int ID { get; set; }

        public int ColorID { get; set; }

        public string BaseColor { get; set; }

        public float Weight { get; set; }

        public Color Color { get; set; }

    }

}
