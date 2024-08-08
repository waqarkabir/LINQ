using LearningLINQWithSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningLINQWithSQL
{
    public class HRDbContext : DbContext
    {
        
        public HRDbContext(DbContextOptions<HRDbContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public IConfiguration Configuration { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Default"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary keys
            modelBuilder.Entity<Employee>().HasKey(e => e.EmployeeId);
            modelBuilder.Entity<Salary>().HasKey(s => s.SalaryId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
