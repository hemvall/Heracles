using Heracles.Models;
using Microsoft.EntityFrameworkCore;

namespace heracles_api.Context
{
    public class HeraclesContext : DbContext
    {
        public DbSet<Exercise>? Exercise { get; set; }
/*        public DbSet<Categoria>? Categorias { get; set; }
*/        public HeraclesContext(DbContextOptions<HeraclesContext> options) : base(options)
        {
        }
    }
}