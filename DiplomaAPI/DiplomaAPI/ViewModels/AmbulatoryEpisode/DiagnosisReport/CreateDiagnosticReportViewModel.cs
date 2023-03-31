namespace DiplomaAPI.ViewModels.AmbulatoryEpisode.DiagnosisReport
{
    public class CreateDiagnosticReportViewModel
    {
        public string ServiceId { get; set; }

        public string Category { get; set; }

        public string Conclusion { get; set; }

        public int ExecutantDoctorId { get; set; }

        public int InterpretedDoctorId { get; set; }
    }
}
