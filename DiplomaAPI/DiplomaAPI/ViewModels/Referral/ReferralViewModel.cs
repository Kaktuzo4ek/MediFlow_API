namespace DiplomaAPI.ViewModels.Referral
{
    public class ReferralViewModel
    {
        public string ReferralId { get; set; }

        public Models.Employee Doctor { get; set; }

        public string Status { get; set; }

        public string ProcessStatus { get; set; }

        public string Priority { get; set; }

        public Models.ReferralCategory Category { get; set; }

        public Models.Service Service { get; set; }

        public Models.Patient Patient { get; set; }

        public DateTime Date { get; set; }

        public DateTime Validity { get; set; }
    }
}
