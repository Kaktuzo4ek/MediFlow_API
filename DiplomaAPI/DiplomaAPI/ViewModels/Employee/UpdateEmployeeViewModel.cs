using DiplomaAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DiplomaAPI.ViewModels.Employee
{
    public class UpdateEmployeeViewModel
    {
        [AllowNull]
        public int? Id { get; set; }

        [AllowNull]
        public string? Surname { get; set; }

        [AllowNull]
        public string? Name { get; set; }

        [AllowNull]
        public string? Patronymic { get; set; }

        [AllowNull]
        public string? PhoneNumber { get; set; }

        [AllowNull]
        public DateTime? DateOfBirth { get; set; }

        [AllowNull]
        public string? Gender { get; set; }

        [AllowNull]
        public string Password { get; set; }
    }
}
