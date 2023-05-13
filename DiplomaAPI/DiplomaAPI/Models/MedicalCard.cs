namespace DiplomaAPI.Models
{
    public class MedicalCard
    {
        public int Id { get; set; }

        public string DirectedFrom { get; set; }

        public string? InstitutionName { get; set; }

        public string? InstitutionCode { get; set; }

        public Models.Referral? Referral { get; set; }

        public Models.Patient Patient { get; set; }

        public string? CountryAndCode { get; set; }

        public string? DocumentType { get; set; }

        public string? DocumentNumber { get; set; }

        public bool IsWorking { get; set; }

        public string? WorkPlace { get; set; }
    }
}
