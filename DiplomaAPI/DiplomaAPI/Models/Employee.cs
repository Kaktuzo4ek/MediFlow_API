using Microsoft.AspNetCore.Identity;

namespace DiplomaAPI.Models
{
    public class Employee : IdentityUser<int>
    {
        public override int Id { get; set; }

        public int InstitutionId { get; set; }

        public int DepartmentId { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int PositionId { get; set; }

        public string Gender { get; set; }

        //public Image Image { get; set; }
    }
}
