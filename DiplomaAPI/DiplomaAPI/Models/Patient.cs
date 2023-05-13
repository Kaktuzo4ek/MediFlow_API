namespace DiplomaAPI.Models
{
    public class Patient
    {
        public int PatientId { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public int? Height { get; set; }

        public int? Weight { get; set; }

        public string DocumentType { get; set; }

        public string DocumentNumber { get; set; }
        
        public string IdentityCode { get; set; }

        public string City { get; set; }

        public string? Region { get; set; }

        public string? District { get; set; }

        public string? Street { get; set; }

        public string? PostIndex { get; set; }

        public string? BuildingNumber { get; set; }

        public string? Email { get; set; }

    }
}
