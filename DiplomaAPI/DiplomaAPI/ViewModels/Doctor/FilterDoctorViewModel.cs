using System.Diagnostics.CodeAnalysis;

namespace DiplomaAPI.ViewModels.Employee
{
    public class FilterDoctorViewModel
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
        public Models.Position? Position { get; set; }
    }
}
