using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Models
{
    public class DiagnosisMKX10AM
    {
        [Key]
        public string DiagnosisId { get; set; }

        public string DiagnosisName { get; set; }

        public Models.DiagnosisMKX10AMCategory Category { get; set; }
    }
}
