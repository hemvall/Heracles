using Heracles.Models;
using Microsoft.EntityFrameworkCore;

namespace heracles_api.Context
{
    public class HeraclesContext : DbContext
    {
        public DbSet<Exercise>? Exercises { get; set; }
        public DbSet<ExerciseType>? ExerciseType { get; set; }
        public DbSet<Performance>? Performances { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Unit>? Units { get; set; }

        public HeraclesContext(DbContextOptions<HeraclesContext> options) : base(options)
        {
        }
    }
}