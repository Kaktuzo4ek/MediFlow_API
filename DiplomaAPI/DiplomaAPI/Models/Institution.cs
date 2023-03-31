namespace DiplomaAPI.Models
{
    public class Institution
    {
        public int InstitutionId { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public Models.Certificate Certificate { get; set; }

        public ICollection<InstitutionAndDepartment> InstitutionsAndDepartments { get; set; }
    }
}
