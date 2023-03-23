namespace DiplomaAPI.ViewModels.AmbulatoryEpisode
{
    public class AmbulatoryEpisodeViewModel
    {
        public int EpisodeId { get; set; }

        public Models.Doctor Doctor { get; set; }

        public Models.Patient Patient { get; set; }

        public Models.Appointment? Appointment { get; set; }

        public Models.DiagnosisMKX10AM? DiagnosisMKX10AM { get; set; }

        public Models.ReferralPackage? ReferralPackage { get; set; }

        public Models.Procedure? Procedure { get; set; }

        public string Status { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
