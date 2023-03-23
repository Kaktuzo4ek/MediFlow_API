using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Models
{
    public class DiagnosisMKX10AMCategory
    {
        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
