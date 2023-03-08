using System.Diagnostics.CodeAnalysis;

namespace DiplomaAPI.ViewModels.Referral
{
    public class CreateReferralViewModel
    {
        [AllowNull]
        public string? ReferralId { get; set; }

        [AllowNull]
        public int? DoctorId { get; set; }

        [AllowNull]
        public int? PatientId { get; set; }

        [AllowNull]
        public int? CategoryId { get; set; }

        [AllowNull]
        public string? ServiceId { get; set; }

        [AllowNull]
        public string? Priority { get; set; }
    }
}
