namespace DiplomaAPI.Models
{
    public class Patient
    {
        public int PatientId { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string City { get; set; }
    }
}
