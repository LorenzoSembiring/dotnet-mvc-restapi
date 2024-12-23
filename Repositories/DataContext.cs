using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class DataContext: DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options): base(options) { }

        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Educations> Educations { get; set; }
        public virtual DbSet<StudentCourses> Student_course { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { 
            base.OnModelCreating(modelBuilder);
        }
    }
}
