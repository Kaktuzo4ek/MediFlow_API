using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Models
{
    public class ServiceCategory
    {
        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
