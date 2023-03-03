using DiplomaAPI.Models;

namespace DiplomaAPI.ViewModels.Employee
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        public virtual string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        //public int InstitutionId { get; set; }

        //public int DepartmentId { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        //public int PositionId { get; set; }

        public string Gender { get; set; }

        public string Password { get; set; }
    }
}
