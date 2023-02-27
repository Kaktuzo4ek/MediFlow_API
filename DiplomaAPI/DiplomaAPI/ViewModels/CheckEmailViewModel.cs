using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.ViewModels
{
    public class CheckEmailViewModel
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
