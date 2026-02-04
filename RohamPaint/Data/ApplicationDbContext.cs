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
        public DbSet<RohamPaint.Models.Unit> Unit { get; set; } = default!;
        public DbSet<RohamPaint.Models.Formul> Formul { get; set; } = default!;
        public DbSet<RohamPaint.Models.Color> Color { get; set; } = default!;
    }
}
