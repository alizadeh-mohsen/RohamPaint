namespace RohamPaint.ViewModels
{
    public class FormulsTableViewModel
    {
        List<ColorFormulViewModel> _formuls;
        public List<ColorFormulViewModel> Formuls
        {
            get { return _formuls.OrderBy(c => c.BaseColor).ToList(); }
            set { _formuls = value; }
        }
        public float TotalWeight => Formuls?.Sum(f => f.Weight) ?? 0;
    }
}
