using DiplomaAPI.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DiplomaAPI.Data
{
    public class DataContext : IdentityDbContext<Employee, IdentityRole<int>, int>
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-HH7EI3P;Database=MediFlow_DB;Integrated Security=true;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().ToTable("Employees");

            modelBuilder.Entity<InstitutionAndDepartment>().HasKey(key => new { key.InstitutionId, key.DepartmentId });
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Institution> Institutions { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<InstitutionAndDepartment> institutionsAndDepartments { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Referral> Referrals { get; set; }

        public DbSet<ReferralCategory> ReferralCategories { get; set; }

        public DbSet<Service> Services { get; set; }
    }
}
