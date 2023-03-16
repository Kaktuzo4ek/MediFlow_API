using System.Collections;

namespace DiplomaAPI.Models
{
    public class Referral
    {
        public int ReferralId { get; set; }

        public string ReferralPackageId { get; set; }

        public string Priority { get; set; }

        public string Status { get; set; }

        public string ProcessStatus { get; set; }

        public Models.Service Service { get; set; }
    }
}
