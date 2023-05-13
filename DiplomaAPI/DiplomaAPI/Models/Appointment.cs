using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DiplomaAPI.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        public DateTime Date { get; set; }

        public ICollection<AppointmentAndDiagnosisICPC2> AppointmentsAndDiagnosesICPC2 { get; set; }

        public Models.Referral? Referral { get; set; }

        public Models.Doctor Doctor { get; set; }

        public string? AppealReasonComment { get; set; }

        public string InteractionClass { get; set; }

        public string Visiting { get; set; }

        public string InteractionType { get; set; }

        public ICollection<AppointmentAndService> AppointmentsAndServices { get; set; }

        public string? ServiceComment { get; set; }

        public string Priority { get; set; }

        public string? Treatment { get; set; }

        public string? Notes { get; set; }
    }
}
