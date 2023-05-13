using System.Diagnostics.CodeAnalysis;

namespace DiplomaAPI.ViewModels.InpatientEpisode
{
    public class UpdateInpatientEpisodeViewModel
    {
        [AllowNull]
        public int? EpisodeId { get; set; }

        [AllowNull]
        public string? DiagnosisId { get; set; }

        [AllowNull]
        public string? Name { get; set; }
    }
}
