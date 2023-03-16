using System.Diagnostics.CodeAnalysis;

namespace DiplomaAPI.ViewModels.ReferralPackage
{
    public class UpdateReferralPackageViewModel
    {
        [AllowNull]
        public string? ReferralId { get; set; }

        [AllowNull]
        public int? CategoryId { get; set; }

        [AllowNull]
        public string? ServiceId { get; set; }

        [AllowNull]
        public string? Priority { get; set; }
    }
}
