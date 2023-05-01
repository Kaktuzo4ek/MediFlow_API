using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Models
{
    public class InpatientEpisode
    {
        [Key]
        public int EpisodeId { get; set; }

        public DateTime ReceiptDate { get; set; }

        public Models.Doctor Doctor { get; set; }

        public Models.Patient Patient { get; set; }

        public string PatientStatus { get; set; }

        public string HospitalizationType { get; set; }

        public Models.Institution Institution { get; set; }

        public Models.Department Department { get; set; }

        public string BedType { get; set; }

        public string? BenefitCategory { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }

        public Models.DiagnosisMKX10AM? DiagnosisMKX10AM { get; set; }

        public Models.ReferralPackage? ReferralPackage { get; set; }

        public ICollection<Procedure>? Procedure { get; set; }

        public ICollection<DiagnosticReport>? DiagnosticReports { get; set; }

        public string Status { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
