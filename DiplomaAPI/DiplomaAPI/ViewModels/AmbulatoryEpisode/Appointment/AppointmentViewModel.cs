using DiplomaAPI.Models;

namespace DiplomaAPI.ViewModels.AmbulatoryEpisode.Appointment
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }

        public DateTime Date { get; set; }

        public ICollection<DiagnosisICPC2> DiagnosesICPC2 { get; set; }

        public string AppealReasonComment { get; set; }

        public string InteractionClass { get; set; }

        public string Visiting { get; set; }

        public string InteractionType { get; set; }

        public ICollection<Service> Services { get; set; }

        public string ServiceComment { get; set; }

        public string Priority { get; set; }

        public string Treatment { get; set; }

        public string Notes { get; set; }
    }
}
