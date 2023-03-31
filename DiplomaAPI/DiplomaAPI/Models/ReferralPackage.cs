namespace DiplomaAPI.Models
{
    public class ReferralPackage
    {
        public string ReferralPackageId { get; set; }

        public Models.Doctor Doctor { get; set; }

        public Models.Patient Patient { get; set; }

        public DateTime Date { get; set; }

        public DateTime Validity { get; set; }

        public string ProcessStatus { get; set; }

        public ICollection<Referral> Referrals { get; set; }
    }
}
