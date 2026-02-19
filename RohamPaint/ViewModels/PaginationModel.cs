using RohamPaint.Modelhelper;

namespace RohamPaint.ViewModels;

public class PaginationModel
{
    public MetaData MetaData { get; set; } = default!;
    public int ItemCount { get; set; }
    public string? Search { get; set; }
}
