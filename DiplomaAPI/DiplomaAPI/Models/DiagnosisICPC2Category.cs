using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Models
{
    public class DiagnosisICPC2Category
    {
        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
