using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RohamPaint.Models;

namespace RohamPaint.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Base> Base { get; set; } = default!;
        public DbSet<BaseColor> BaseColor { get; set; } = default!;
        public DbSet<Car> Car { get; set; } = default!;
        public DbSet<ColorType> ColorType { get; set; } = default!;
        public DbSet<Unit> Unit { get; set; } = default!;
        public DbSet<Formul> Formul { get; set; } = default!;
        public DbSet<Color> Color { get; set; } = default!;
        public DbSet<RohamPaint.ViewModels.ColorCreateViewModel> ColorCreateViewModel { get; set; } = default!;
    }
}
