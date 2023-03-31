using DiplomaAPI.Models;
using System.Diagnostics.CodeAnalysis;

namespace DiplomaAPI.ViewModels.ReferralPackage
{
    public class CreateReferralPackageViewModel
    {
        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        public string Priority { get; set; }

        public string[] Services { get; set; }
    }
}
