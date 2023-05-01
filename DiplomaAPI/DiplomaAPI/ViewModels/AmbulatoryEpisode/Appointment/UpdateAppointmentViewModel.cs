using System.Diagnostics.CodeAnalysis;

namespace DiplomaAPI.ViewModels.AmbulatoryEpisode.Appointment
{
    public class UpdateAppointmentViewModel
    {
        public int AppointmentId { get; set; }

        [AllowNull]
        public string? ReferralId { get; set; }

        [AllowNull]
        public int[]? DiagnosesICPC2 { get; set; }

        [AllowNull]
        public string? AppealReasonComment { get; set; }

        [AllowNull]
        public string? InteractionClass { get; set; }

        [AllowNull]
        public string? Visiting { get; set; }

        [AllowNull]
        public string? InteractionType { get; set; }

        [AllowNull]
        public string[]? Services { get; set; }

        [AllowNull]
        public string? ServiceComment { get; set; }

        [AllowNull]
        public string? Priority { get; set; }

        [AllowNull]
        public string? Treatment { get; set; }

        [AllowNull]
        public string? Notes { get; set; }
    }
}
