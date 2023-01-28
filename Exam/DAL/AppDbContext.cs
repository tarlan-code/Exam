using Exam.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>().HasIndex(p => p.Name).IsUnique();
            base.OnModelCreating(modelBuilder);
        }

    }
}
