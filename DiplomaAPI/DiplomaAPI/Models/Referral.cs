using System.Collections;

namespace DiplomaAPI.Models
{
    public class Referral
    {
        public int ReferralId { get; set; }

        public Models.Doctor Doctor { get; set; }

        public Models.Patient Patient { get; set; }

        public DateTime Date { get; set; }

        public DateTime Validity { get; set; }

        public string ReferralPackageId { get; set; }

        public string Priority { get; set; }

        public string Status { get; set; }

        public string ProcessStatus { get; set; }

        public string Category { get; set; }

        public Models.Department? HospitalizationDepartment { get; set; }

        public Models.Service Service { get; set; }
    }
}
