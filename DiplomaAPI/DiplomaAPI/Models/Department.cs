namespace DiplomaAPI.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public ICollection<InstitutionAndDepartment> InstitutionsAndDepartments { get; set; }
    }
}
