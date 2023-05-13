using DiplomaAPI.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DiplomaAPI.Data
{
    public class DataContext : IdentityDbContext<Doctor, IdentityRole<int>, int>
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

            modelBuilder.Entity<Doctor>().ToTable("Doctors");

            modelBuilder.Entity<InstitutionAndDepartment>().HasKey(key => new { key.InstitutionId, key.DepartmentId });

            modelBuilder.Entity<AppointmentAndService>().HasKey(key => new { key.AppointmentId, key.ServiceId });

            modelBuilder.Entity<AppointmentAndDiagnosisICPC2>().HasKey(key => new { key.AppointmentId, key.DiagnosisId });
        }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Institution> Institutions { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<InstitutionAndDepartment> InstitutionsAndDepartments { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<ReferralPackage> ReferralPackages { get; set; }

        public DbSet<Referral> Referrals { get; set; }

        public DbSet<ServiceCategory> ServiceCategories { get; set; }

        public DbSet<AppointmentAndService> AppointmentsAndServices { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Procedure> Procedures { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Certificate> Certificates { get; set; }

        public DbSet<DiagnosisMKX10AM> DiagnosesMKX10AM { get; set; }

        public DbSet<DiagnosisMKX10AMCategory> DiagnosesMKX10AMCategories { get; set; }

        public DbSet<DiagnosisICPC2> DiagnosesICPC2 { get; set;}

        public DbSet<DiagnosisICPC2Category> DiagnosesICPC2Categories { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<AmbulatoryEpisode> AmbulatoryEpisodes { get; set; }

        public DbSet<DiagnosticReport> DiagnosticReports { get; set; }

        public DbSet<AppointmentAndDiagnosisICPC2> AppointmentsAndDiagnosesICPC2 { get; set; }

        public DbSet<InpatientEpisode> InpatientEpisodes { get; set;}

        public DbSet<MedicalCard> MedicalCards { get; set; }
    }
}
