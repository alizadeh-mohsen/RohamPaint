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
        public DbSet<RohamPaint.Models.Base> Base { get; set; } = default!;
        public DbSet<RohamPaint.Models.BaseColor> BaseColor { get; set; } = default!;
        public DbSet<RohamPaint.Models.Make> Make { get; set; } = default!;
    }
}
