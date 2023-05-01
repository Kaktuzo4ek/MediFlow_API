using DiplomaAPI.Models;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        public List<Department> getAll();

        public Department getDepartment(int id);

        public List<InstitutionAndDepartment> getInstitutionAndDepartment(int id);

        public List<Department> getDepartmentsByInstitution(int id);
    }
}
