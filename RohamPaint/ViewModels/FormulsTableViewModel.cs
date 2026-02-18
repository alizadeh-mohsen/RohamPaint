namespace RohamPaint.ViewModels
{
    public class FormulsTableViewModel
    {
        public List<ColorFormulViewModel> Formuls { get; set; }
        public float TotalWeight => Formuls?.Sum(f => f.Weight) ?? 0;
    }
}
