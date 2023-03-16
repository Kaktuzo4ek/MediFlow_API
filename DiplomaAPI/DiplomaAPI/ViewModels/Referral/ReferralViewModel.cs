namespace DiplomaAPI.ViewModels.Referral
{
    public class ReferralViewModel
    {
        public int ReferralId { get; set; }

        public string ReferralPackageId { get; set; }

        public string Priority { get; set; }

        public string Status { get; set; }

        public string ProcessStatus { get; set; }

        public Models.Service Service { get; set; }
    }
}
