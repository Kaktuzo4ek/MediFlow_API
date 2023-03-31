using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Models
{
    public class AppointmentAndDiagnosisICPC2
    {
        [Key]
        public int AppointmentId { get; set; }

        public Models.Appointment Appointment { get; set; }

        [Key]
        public int DiagnosisId { get; set; }

        public Models.DiagnosisICPC2 DiagnosisICPC2 { get; set; }
    }
}
