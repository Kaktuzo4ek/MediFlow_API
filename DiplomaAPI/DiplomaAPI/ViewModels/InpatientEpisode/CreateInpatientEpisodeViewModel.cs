namespace DiplomaAPI.ViewModels.InpatientEpisode
{
    public class CreateInpatientEpisodeViewModel
    {
        public string ReceiptDate { get; set; }

        public string BedType { get; set; }

        public string BenefitCategory { get; set; }

        public Models.Patient PatientInfo { get; set; }

        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        // For Medical Card Start

        public string DirectedFrom { get; set; }

        public string? InstitutionName { get; set; }

        public string? InstitutionCode { get; set; }

        public string? CountryAndCode { get; set; }

        public string? DocumentType { get; set; }

        public string? DocumentNumber { get; set; }

        public bool IsWorking { get; set; }

        public string? WorkPlace { get; set; }

        // For Medical Card End

        public string ReferralPackageId { get; set; }

        public string Type { get; set; }
    }
}
