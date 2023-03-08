using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Models
{
    public class ReferralCategory
    {
        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
