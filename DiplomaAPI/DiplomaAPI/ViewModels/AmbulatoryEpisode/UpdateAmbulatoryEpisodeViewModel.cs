using System.Diagnostics.CodeAnalysis;

namespace DiplomaAPI.ViewModels.AmbulatoryEpisode
{
    public class UpdateAmbulatoryEpisodeViewModel
    {
        [AllowNull]
        public int? EpisodeId { get; set; }

        [AllowNull]
        public string? DiagnosisId { get; set; }

        [AllowNull]
        public string? Name { get; set; }

        [AllowNull]
        public string? Type { get; set; }
    }
}
