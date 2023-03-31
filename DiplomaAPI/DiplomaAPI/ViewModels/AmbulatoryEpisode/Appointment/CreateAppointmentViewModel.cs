using DiplomaAPI.Models;
using System.Diagnostics.CodeAnalysis;

namespace DiplomaAPI.ViewModels.AmbulatoryEpisode.Appointment
{
    public class CreateAppointmentViewModel
    {
        public string ReferralId { get; set; }

        public int[] DiagnosesICPC2 { get; set; }

        public int AmbulatoryEpisodeId { get; set; }

        [AllowNull]
        public string? AppealReasonComment { get; set; }

        public string InteractionClass { get; set; }

        public string Visiting { get; set; }

        public string InteractionType { get; set; }

        public string[] Services { get; set; }

        [AllowNull]
        public string? ServiceComment { get; set; }

        public string Priority { get; set; }

        [AllowNull]
        public string? Treatment { get; set; }

        [AllowNull]
        public string? Notes { get; set; }
    }
}
