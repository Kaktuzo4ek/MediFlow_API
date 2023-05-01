namespace DiplomaAPI.ViewModels.Procedure
{
    public class CreateProcedureViewModel
    {
        public string ReferralPackageId { get; set; }

        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        public string ServiceId { get; set; }

        public string Status { get; set; }

        public string? Notes { get; set; }
    }
}
