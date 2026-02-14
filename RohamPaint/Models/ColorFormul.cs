namespace RohamPaint.Models
{
    public class ColorFormul
    {
        public int ID { get; set; }

        public int ColorID { get; set; }

        public string BaseCode { get; set; }

        public float Weight { get; set; }

        public virtual Color Color { get; set; }

    }

}
