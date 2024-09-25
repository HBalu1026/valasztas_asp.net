using Microsoft.EntityFrameworkCore;

namespace valasztas.Modells
{
    public class ValasztasDbContext : DbContext
    {
        public ValasztasDbContext(DbContextOptions<ValasztasDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<Jelolt> JeloltekListaja { get; set; }
        public DbSet<Part> Partok {  get; set; }
    }
}
