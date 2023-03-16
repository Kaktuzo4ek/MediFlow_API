using DiplomaAPI.Models;

namespace DiplomaAPI.ViewModels.ReferralPackage
{
    public class ReferralPackageViewModel
    {
        public string ReferralPackageId { get; set; }

        public Models.Doctor Doctor { get; set; }

        public Models.Patient Patient { get; set; }

        public DateTime Date { get; set; }

        public DateTime Validity { get; set; }

        public ICollection<Models.Referral> Referrals { get; set; }
    }
}
