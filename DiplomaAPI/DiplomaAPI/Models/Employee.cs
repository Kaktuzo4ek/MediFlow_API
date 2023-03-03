using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaAPI.Models
{
    public class Employee : IdentityUser<int>
    {
        public override int Id { get; set; }

        public Models.Institution Institution { get; set; }

        public Models.Department Department { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Models.Position Position { get; set; }

        public string Gender { get; set; }
    }
}
