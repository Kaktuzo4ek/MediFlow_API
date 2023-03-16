using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaAPI.Models
{
    public class InstitutionAndDepartment
    {
        public int InstitutionId { get; set; }

        public Models.Institution Institution { get; set; }
      
        public int DepartmentId { get; set; }

        public Models.Department Department { get; set; }
    }
}
