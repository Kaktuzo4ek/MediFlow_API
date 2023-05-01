namespace DiplomaAPI.ViewModels.AmbulatoryEpisode.DiagnosisReport
{
    public class DiagnosticReportViewModel
    {
        public int ReportId { get; set; }

        public Models.Patient Patient { get; set; }

        public Models.Service Service { get; set; }

        public string Category { get; set; }

        public string Conclusion { get; set; }

        public DateTime Date { get; set; }

        public Models.Doctor ExecutantDoctor { get; set; }

        public Models.Doctor InterpretedDoctor { get; set; }
    }
}
