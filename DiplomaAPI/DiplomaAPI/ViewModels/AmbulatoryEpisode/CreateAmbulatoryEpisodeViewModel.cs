using System.Diagnostics.CodeAnalysis;

namespace DiplomaAPI.ViewModels.AmbulatoryEpisode
{
    public class CreateAmbulatoryEpisodeViewModel
    {
        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        [AllowNull]
        public string? DiagnosisId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}
