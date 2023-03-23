using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Models
{
    public class DiagnosisICPC2
    {
        [Key]
        public int DiagnosisId { get; set; }

        public string DiagnosisCode { get; set; }

        public string DiagnosisName { get; set;}

        public Models.DiagnosisICPC2Category Category { get; set;}
    }
}
