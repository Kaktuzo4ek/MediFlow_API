using System.Diagnostics.CodeAnalysis;

namespace DiplomaAPI.ViewModels.Referral
{
    public class UpdateReferralViewModel
    {
        [AllowNull]
        public int? ReferralId { get; set; }

        [AllowNull]
        public string? Priority { get; set; }

        [AllowNull]
        public string? ServiceId { get; set; }

        [AllowNull]
        public string? Category { get; set; }

        [AllowNull]
        public int? HospitalizationDepartmentId { get; set; }
    }
}
